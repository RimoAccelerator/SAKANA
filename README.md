# SAKANA：年轻人第一台循环伏安的全面制造指南

这是一款基于树莓派Pico的电化学工作站，其中包含了所有软硬件信息。

目录结构：

- Hardware
  
  Gerber_SAKANA_PCB.zip： PCB制板文件
  
  SAKANA_PCB.epro：PCB设计文件（基于立创EDA）
  
  Shell.zip：外壳模型，可进行3D打印

- Sources
  
  SAKANA_Pico.py：Pico上的Micropython文件
  
  SakanaController：上位机控制软件源代码

鉴于绝大多数化学工作者都像我一样，没有接受过系统的电子工程教育，本文中会详细介绍电路的设计过程。如果你有此类基础，可直接跳转到**6. 建立连接**。

1. **背景介绍**  
   
   现代电化学工作站基于运算放大器（简称运放）进行设计，主要在20世纪70年代发展起来。早年文献中对电化学工作站的设计原则、特别是电路稳定性问题进行了诸多讨论，这些讨论对电路知识有较高的要求，有兴趣的读者可以自行查阅。近十多年来，得益于单片机技术的普及，使用以Arduino为代表的单片机控制从而低成本制造电化学工作站的案例不断见诸报道，以至于到了相当丰富的程度。甚至已经有学校将自制电化学工作站纳入了分析化学实验课程中（https://www.chem.uci.edu/~unicorn/243/labs/W2019Chem243Lab3.pdf）。
   
   ![图片](https://mmbiz.qpic.cn/mmbiz_png/nmMUPuaJVp6uGV9dXXdZMmBuotF1vW8XXgxdz8KxiaFLc8b3Dfzt7cfT9Bib3nnfGFRS1ATDdDm27FqHdWN3Vrnw/640?wx_fmt=png&tp=webp&wxfrom=5&wx_lazy=1&wx_co=1)
   
   本文的设计原型来自2016年Meloni的工作（J. Chem. Educ. 2016, 93, 7, 1320–1322）。Meloni报道了基于Arduino的电化学工作站设计，并给出了电路图和简易源代码。然而不幸的是，虽然Meloni文中呈现了良好的结果，但如果直接照搬它的设计，仔细考察，就会发现其实并不能用。接下来我们会展示这一电路的原理、以及本文的装置的设计和改进过程。

2. Meloni原型机的原理
   
   用于循环伏安的电化学工作站基于三电极系统。对三电极系统的理解是仪器分析课程的基本内容，本文不再赘述。一个合格的电化学工作站需要确保以下几点：
   
   a. 在参比电极(RE)上没有电流流过；
   
   b. 能精确控制工作电极（WE）相对于RE的电位，并且这一电位需要以最高几个Hz的频率变化；
   
   c. 能精确测量通过WE的电流，电流的量级在零点几到几百uA之间。

![图片](https://mmbiz.qpic.cn/mmbiz_png/nmMUPuaJVp6uGV9dXXdZMmBuotF1vW8XpZBqTcMIWia6Cr39UibSB8iaH4qcX5Gko6ZlYl3F0wr57UePv7dQVlGPQ/640?wx_fmt=png&tp=webp&wxfrom=5&wx_lazy=1&wx_co=1)

以上是Meloni原型机的原理图。接下来分析该电路的各组成部分。  

a. 电压控制电路  

运放是现代电化学工作站的核心，要理解电压控制电路必须首先理解运放。

![图片](https://mmbiz.qpic.cn/mmbiz_png/nmMUPuaJVp6uGV9dXXdZMmBuotF1vW8XXNlRCYcn4JKygH3q8dEoWD8ricnQbB1Wc9ZWZmicLD2fzUoibXdTzLMjg/640?wx_fmt=png&tp=webp&wxfrom=5&wx_lazy=1&wx_co=1)

一个运放至少有5个引脚：2个电源引脚（图中正负6.5 V），一个同相输入端（图中+），一个反相输入端（图中-），一个输出端。当输出端与反相输入端通过电阻连接时，构成了负反馈电路；对于理想运放，输入端可视为无穷大的电阻，而一旦构成负反馈，则两个输入端电位相等且都没有电流流过。因此对于上图中左侧的运放，输出电压Vin始终等于同相输入端电位，这被称为跟随器，可对电路起到隔离作用。接下来为了解出第二个运放的输出电位Vo，可设通过R4的电流为i，则Vo = -iR4，可知运放对电流有放大作用，放大倍数取决于反馈电阻。由于通过反相输入端的电流为0，可知i = Vin / R2 - 5 V / R2，进而可求出Vo。通过控制Vin = V1，即可控制Vo；随后Vo接到UA3上，只要RE和CE放在电解池中，就再次构成负反馈，RE上的电压即等于V1。  

之所以要采用这样的设计，是由于多数单片机只能输出正电压，因此需要通过R3施加一个负电压偏置来将其转换为循环伏安所需要的电压范围。在Meloni原型机中，V1通过Arduino的PWM供给。PWM是通过让输出信号在0 V和（对于Arduino来说）5 V间快速切换产生方波的技术，假设占空比为a，则等效输出5 V * a的电压。为了将这种方波信号转换为循环伏安所需要的直流信号，Meloni原型机在R1和UA1之间对地接入电容C1，从而构成了RC滤波电路。RC滤波电路对电信号的阻抗随频率上升而快速增加，对于频率大于1/(2pi*RC)的信号，通过RC滤波器后衰减大于3 dB，这被称为截止频率。方波信号经过傅里叶展开后，可视为直流信号叠加一系列高频信号；理想上，当高频信号被充分滤除，就可以得到5 V * a的直流电压。随后经过UA1~UA3，最终反映到RE对地的电位上。

类似地，UA2上的C2同样与R4构成RC滤波器，从而尽可能滤除运放带来的噪声。

b. 电流采样电路

![图片](https://mmbiz.qpic.cn/mmbiz_png/nmMUPuaJVp6uGV9dXXdZMmBuotF1vW8Xl5CgeX5Wib0XGXrjticzkibe6INLKSjvmrYuvBSOYv9blicrg0BowCPUpw/640?wx_fmt=png&tp=webp&wxfrom=5&wx_lazy=1&wx_co=1)

Meloni原型机中，电流采样电路接在CE上，通过R5施加5 V的偏置，接到运放上，通过R6负反馈，使得CE的电位恒为0。两者混合后的电流i被放大为电位iR6（恒为正，同样是由于单片机只能读取正电位），经过一个跟随器后进入Arduino的ADC引脚（Analog to Digital）从而被读取。读完上文后，相信读者已经会计算通过CE的电流与ADC读数的关系了。

你发现了什么不对了吗？

在Meloni原型机中，CE和WE的位置画反了。我们并不关心CE上的电位是多少，而必须控制WE和RE的电位差，因此必须将WE接在电流采集电路处，从而使得上面控制的RE相对于地的电位等同于WE相对于RE的电位的负值。  

总之，现在你已经理解了Meloni原型机的原理，接下来就开始实现吧。

**3. 材料清单**  

- 1 * 树莓派Pico

- 1 * MCP4725模块

- 1 * ADS1115模块

- 1 * INA219芯片

- 1 * 6 V输出DC/DC升压模块

- 2 * NE5532或OP297运放

- 4 * 继电器模块

- 1 * CH340串口模块

- 若干电容电阻
  
  总成本在100元左右。
  
  ![](C:\Users\Administrator\AppData\Roaming\marktext\images\2025-01-15-17-22-39-d6e246e1-22d1-47d0-b3b9-ecc9351ef574.png)
  
  （没有任何淘宝店铺赞助本文）

Meloni使用了Arduino，而本文使用树莓派Pico。Pico的性能更为优异，而价格却更加低廉。值得注意的是Pico的输出电压是3.3 V（另有一个5 V引脚），因此基于输出5 V的Arduino的电路中元件取值要重新换算。  

**4. Meloni原型机的实现和失败，与第一版可工作的电路**  

Meloni已经给出了完整的电路图，将其实现是十分简单的事情，不再赘述。唯一值得一提的是，电路中需要使用负电压，仅使用Pico无法做到，本文中使用了升压电源模块得到正负6 V。正电压非常标准，而负电压实际大约为-5.5 V。它同样用于给运放供电。绝大多数运放的输出电压都位于两个电源引脚的电压之间，通常是一个较窄的范围（这将成为日后电阻选值的考量），选择轨到轨运放可以输出到接近供电电压的范围。

然而很快就会发现，这套电路的立足之本，即通过对PWM滤波以控制RE上的电位的做法完全不可行。一方面，即使经过滤波，电压仍然会不可避免地波动（纹波现象），而另一方面的问题则更加严重：

<img title="" src="https://mmbiz.qpic.cn/mmbiz_png/nmMUPuaJVp6uGV9dXXdZMmBuotF1vW8XibaUULzxuv1D6jPsFGphPGQthiaYxR9z5IibxDYib1hzWNPbf9YV2V8fvA/640?wx_fmt=png&tp=webp&wxfrom=5&wx_lazy=1&wx_co=1" alt="图片" width="458">

上图展示了PWM占空比与经过滤波得到的电压的关系。理想情况下，电压应随占空比呈现线性，但事实完全不同，呈现出一条十分不规则的曲线。这种情况下，通过PWM控制RE上的电压完全不可靠！  

幸运的是，由于现代电子工业高度发达，许多电路中的功能可以方便地找到模块，像搭积木一样处理。事实上，如果想输出可调的电压，并不需要手动设计电源电路，使用（3）中展示的MCP4725模块即可。它基于I2C协议和Pico连接，可以方便地使用Github上现成的库进行控制。经过实测，它可以精确、稳定地输出0 ~ 4.5 V的直流电压。

![图片](https://mmbiz.qpic.cn/mmbiz_png/nmMUPuaJVp6uGV9dXXdZMmBuotF1vW8X9wFMpWJwhaqBDnWaAB70twe6N2F3mWLsE2iaV2m4iczVkFeqXFfc4yMw/640?wx_fmt=png&tp=webp&wxfrom=5&wx_lazy=1&wx_co=1)

使用MCP4725后，采用了如上电路以适配Pico的性能。在面包板上搭建好后，已经可以用于测定盐水中的赤血盐的CV：  

![图片](https://mmbiz.qpic.cn/mmbiz_png/nmMUPuaJVp6uGV9dXXdZMmBuotF1vW8XTV2QAGKTcIYaJlDtRcU8r6UZOx3XQk1Cdia4krKBuqAvNRgXicFtOZNw/640?wx_fmt=png&tp=webp&wxfrom=5&wx_lazy=1&wx_co=1)

**5. 进一步改进：重做电流采样**  

上述电路如果作为教学实验，则已经可以了；但如果要用于实际测量，还存在诸多问题。其中最大的问题在于噪声。事实上，Pico ADC的噪声是一个世界性难题，许多人都受此困扰而难以找到解决方案。以下随机展示一个ADC面对直流电的测量结果，可见存在相当程度的不规则的跳变。这一噪声完全来自Pico自身的缺陷，无法被任何外部电路滤除。  

![图片](https://mmbiz.qpic.cn/mmbiz_png/nmMUPuaJVp6uGV9dXXdZMmBuotF1vW8XoERcELXmLsXxEwicNYYNicicNp1vLFABkE6wE8vtB4eOmib7m36ElG2zYg/640?wx_fmt=png&tp=webp&wxfrom=5&wx_lazy=1&wx_co=1)

尽管Pico手册上给出了改进ADC的建议，但最终它们没能起效，因此接下来需要放弃Pico自带的ADC，采用其他模块。在这里，需要用到两个模块：ADS1115电压采集模块，以及INA219电流采集模块。两者都使用I2C协议，和MCP4725共用2根线进行通信。

事实证明，ADS1115的精度良好，可以非常平稳地测定接入的电压，并且可以测定不低于-0.5 V的负压。但问题在于采样频率，ADS1115每秒只能进行860次采样，无法满足CV测试在很短的时间内连续变化电压并进行采样的需求，如果硬上会造成死机。但它可以用于电路的校准：  

![图片](https://mmbiz.qpic.cn/mmbiz_png/nmMUPuaJVp6uGV9dXXdZMmBuotF1vW8XCM6rjWoeuMS4uOx4u10sDdn1ewnqtkXia7LsgQt0DAHPZpSv9YJJsrg/640?wx_fmt=png&tp=webp&wxfrom=5&wx_lazy=1&wx_co=1)

将RE和CE的接线端连起来（必须这样做，否则无法构成负反馈），用ADS1115测量RE处的电压，即可对仪器进行校准。  

![图片](https://mmbiz.qpic.cn/mmbiz_png/nmMUPuaJVp6uGV9dXXdZMmBuotF1vW8XkGIzrs9eibK2X9rDseSib9zsu7tWy0ovCQBZg8VNjk4fvlziaY49dN27Q/640?wx_fmt=png&tp=webp&wxfrom=5&wx_lazy=1&wx_co=1)

注意当电压小于-0.5 V时，ADS1115不再能正常工作，而且不仅自己不干活，还会干扰别人，导致RE上的电压异常，因此ADS1115只能用于校准，CV测量时必须同RE断开。

而接下来的INA219将成为电流采样的核心。它与ADC的思路不同，是直接测量电流。INA219本身是个集成电路，需要搭配外围电路使用，商业可得的INA219模块已经集成了一个0.1 Ohm的采样电阻，它通过测定采样电阻上的电压来给出电流，支持正负电流。量程可调，最小量程为400 mA，分辨率为12位，即400/(2^12)~0.1 mA。  

INA219的引入可以使得我们直接去掉电流采样电路冗杂的偏置电压设计，只要将流过WE的电流放大到合适的范围，让INA219采样即可。利用这种思路，在简单尝试后得到了十分积极的初步结果。其中黑线为原始信号，红线为经过平滑后的结果。

<img src="https://mmbiz.qpic.cn/mmbiz_png/nmMUPuaJVp6uGV9dXXdZMmBuotF1vW8XFFvcK1f4IuvfZCFLgYXMZARiaJy3DCwy1ssP6vQmALVvxCXd1KpoiaxA/640?wx_fmt=png&tp=webp&wxfrom=5&wx_lazy=1&wx_co=1" title="" alt="图片" width="400">

以下为SAKANA最终确定使用的电路：

![](C:\Users\Administrator\AppData\Roaming\marktext\images\2025-01-15-16-01-50-a6b8b46e-f6e6-4a12-9fc1-64e141e69383.png)在这一电路中，流过WE的电流i经过一个跨阻放大器，转化为iR的电压信号，其中R可在R4到R6之间选择。经过R7后，通过INA219的实际电流为iR/R7，测量这一电流信号即可换算为流过RE的电流。为了确保INA219的测量精度，iR/R7不能太小，需要在几个mA量级；同时iR又不能太大，必须落在运放能够可靠工作的电压范围内。由于CV波的电流从零点几到几百uA，跨越了3个数量级，采用两个继电器控制接入从6.8 k到680 k之间的电阻以改变量程。

OpAmp2和OpmAmp4的反馈回路上都并联有电容，起到增加运放稳定性和减少输出噪波的作用。借助示波器，可以清晰地看到C1对于确保RE上电压的稳定性有至关重要的作用。没有C1时，RE上的电压会以几百mV的幅度波动，而适当大小的C1能够将这一振幅抑制到不超过几十个mV。

![](C:\Users\Administrator\AppData\Roaming\marktext\images\2025-01-15-16-02-42-7f272320-4ab5-4da9-a151-d59e3c3778f6.png)

C2同样可以显著抑制噪声。此处经过大量尝试，确定C2取值为100 nF，且不随接入的电阻而变化。较大的C2虽然可以进一步使得输出信号变得平滑，但由于滤除了太多快速变化的信号，导致波形畸变、以及更严重的是当改变设定的输出电压时需要相当长的时间才能达到稳定。

R7是一个1 kOhm的电阻。在设计初期，在R7处曾经使用过更小的电阻，寄希望于它能够使得流过INA219的电流尽可能大，以降低测定误差的影响。但不幸的是，较小的R7会导致测定曲线出现未曾预料的巨幅波动。以下是R7 = 100 Ohm的典型图形：

![](C:\Users\Administrator\AppData\Roaming\marktext\images\2025-01-15-16-08-44-image.png)

经过大量尝试，确定R7为1 kOhm时，不会出现振荡现象。为了提高INA219的测量精度并与当前电阻的取值匹配，需要将它并联的采样电阻从0.1 Ohm改成10 Ohm。为了实现这一改动，既可以买来商品INA219模块，手动取下采样电阻后更换：

![](C:\Users\Administrator\AppData\Roaming\marktext\images\2025-01-15-16-55-41-3c050c0a-ffa1-429d-a6fe-5e06c24168d4.png)

也可以重新做一个自己的PCB：

![](C:\Users\Administrator\AppData\Roaming\marktext\images\2025-01-15-16-55-05-image.png)

**6. 建立连接**

在面包板上，将Pico与各模块连接，采用如下接线图（只标出模块之间的连接）：

![](C:\Users\Administrator\AppData\Roaming\marktext\images\2025-01-15-16-45-12-4b5a2caf-8a4f-48bb-9029-5efc0e240b4f.png)

其余部分连接可参照如下原理图：

![](C:\Users\Administrator\AppData\Roaming\marktext\images\2025-01-15-16-50-50-a5fb1c56-6d09-48ff-86bb-6d677e635a7c.png)

在这里使用了4个继电器，其中2个用于控制量程，1个用于控制ADS1115只在希望进行电压校准时接到RE上（进行CV测定时必须断开，否则RE在负电压时不对），1个用于控制电流采集电路（只在进行CV测定时接上，否则电解池会被持续电解）。采用一个CH340串口模块实现与计算机的通信。连接完成后效果如下，可以使用配套软件非常方便地调试。

![](C:\Users\Administrator\AppData\Roaming\marktext\images\2025-01-15-16-11-12-image.png)

**7. 配套软件**

![](C:\Users\Administrator\AppData\Roaming\marktext\images\2025-01-15-16-18-25-d1b1722b-fea9-4143-a09f-15d3c4db78a4.png)

SakanaController是针对Sakana的控制程序。在使用时，首先在Connection区域找到正确的串口并连接，然后在Calibration区域点击Start进行校准。正常情况下，应是一条斜向下的直线（第一个点处存在跳变，属于正常，不会带来任何影响）。此处的横坐标是MCP4725的输出电压，纵坐标为RE的电压，两者线性度非常好，可以在任何时候点击stop提前停止校准。校准完成后，将得到MCP4725电压与V_RE的相关系数，用于后续CV测试。

![](C:\Users\Administrator\AppData\Roaming\marktext\images\2025-01-15-17-07-18-image.png)

在完成校准后，在Measurement区域设置测试参数，点击Start，测试就会开始，曲线实时更新。以下为部分结果案例。

*KCl中的黄血盐*

![](C:\Users\Administrator\AppData\Roaming\marktext\images\2025-01-15-17-13-16-3a6e50b1-2c1f-4190-966f-fbfb8e931410.png)

*DCM中的二茂铁*

![](C:\Users\Administrator\AppData\Roaming\marktext\images\2025-01-15-17-12-35-4ea0aac2-f19d-4c3a-a152-72a0db8244e7.png)

测试完成后，点击Save，可以保存到纯文本文件。

除了CV测量外，软件还有恒电位仪模式。完成校准后，在Potentiostat处输入期望的电压，点击Start，仪器就会让WE相对于RE的电位等于设置数值，可用于电解实验。对于焊接到PCB上的仪器，电压范围可达正负2.5 V，误差在30 mV以内。

**8. 转移到PCB**  

在面包板上建立的电路虽然方便调试，但接触不良的现象非常严重，只要轻微触碰，仪器的表现就会改变，而焊接到PCB板上后就将得到非常可靠的成品。  

![](C:\Users\Administrator\AppData\Roaming\marktext\images\2025-01-15-16-58-10-70f4ab42-d3dd-44b6-a3cd-9b312918116a.png)

绘制好原理图和PCB后，进行生产、焊接即可。

<img src="file:///C:/Users/Administrator/AppData/Roaming/marktext/images/2025-01-15-17-03-18-181aba94f3e1acdf3edbc26271795051.jpeg" title="" alt="" width="556">

![](C:\Users\Administrator\AppData\Roaming\marktext\images\2025-01-15-17-04-48-image.png)

至此，一个用于循环伏安的电化学工作站就完成了。

**关于**

这个项目的名称是SAKANA，发音与Cyclic相似。

非常鼓励读者将本文所介绍的内容用于自己的教学和研究中。凡引用本文者，必须标明出处。凡进行发表、或基于本项目进行二次开发者，必须引用Github页面。

*BSJ INSTRUMENT 泊州仪器*

2025年1月15日

![图片](https://mmbiz.qpic.cn/mmbiz_png/nmMUPuaJVp6uGV9dXXdZMmBuotF1vW8Xp7SxfamfmwyElNNRpb3ibNUibKib6sxO2SiaKDQfWRKymQpkzWcgcgLmOQ/640?wx_fmt=png&tp=webp&wxfrom=5&wx_lazy=1&wx_co=1)

本项目使用了如下Github仓库中的成果：

[chrisb2/pyb_ina219: This library for MicroPython makes it easy to leverage the complex functionality of the Texas Instruments INA219 sensor to measure voltage, current and power.](https://github.com/chrisb2/pyb_ina219)

[robert-hh/ads1x15: Micropython driver for ADS1115 and ADS1015](https://github.com/robert-hh/ads1x15)
