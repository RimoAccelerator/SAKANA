# 电化学工作站SAKANA使用说明书

## 物品清单

- 机器盒 尺寸约11 cm \*11 cm \*5 cm

- 3根母头鳄鱼夹

- 1个串口转USB接头（CH340），已经与机器相连。

- 1个Type-C或Micropython电源线（以下简称电源线）

机器盒前侧面，有电源线插头的一侧，伸出两根串口线，与CH340已经预先接好。

机器盒另一侧面处，有3根电极线，对应参比电极（RE）、工作电极（WE）、对电极（CE），可通过鳄鱼夹与电极相连。

## 典型参数

电压范围：-2.7 V ~ +2.2 V

电压误差范围：±30 mV

最大电流：~800 uA

电流误差范围：<±1 uA

## 使用方法

1. 在电脑上安装CH340驱动。下载地址：[www.wch.cn/download/CH341SER_EXE.html](https://www.wch.cn/download/CH341SER_EXE.html)

2. 在电脑上下载最新控制软件。软件下载地址：[github.com/RimoAccelerator/SAKANA/releases/tag/Windows](https://github.com/RimoAccelerator/SAKANA/releases/tag/Windows)
   
   注意：
   
   本软件需要.NET Framework 8或更高版本；
   
   如果你使用笔记本电脑，为了流畅运行软件，请给笔记本接上电源。

3. 将CH340插入电脑USB口，此时CH340红灯亮起。如果使用了USB扩展坞，请注意有的扩展坞容易松动，此时可以多换几个插口。
   
   *CH340与机器的连线方法：* 如果连线不幸断开，可将两根串口线分别接到CH340的RX和TX两个引脚。如果没有做过标记，区分两根线的方法为：打开机器盒顶盖，在PCB板上标有RX的线，接在CH340的TX引脚；在PCB板上标有TX的线，接在CH340的RX引脚。当然也可以随机连接，如果连接错误，在软件上点击Connect后能成功识别，但在校准、测量等操作时没有反应，此时只需对换两根线即可。

4. 使用电源线给机器供电。电源电压为5 V，可以插在电脑的USB接头上，也可以插在手机USB充电器上。机器预先烧录了Micropython代码，上电后会听到“啪”的一声，是继电器启动的正常声音。如果打开盒子，会发现盒内树莓派Pico亮黄灯，盒内4个继电器中有2个亮蓝灯，说明正常开启。

5. 运行控制软件。在COM列表中列出了检测到连接的所有串口设备。如果将电源线也连接到了电脑，则至少有电源线、CH340这两个；如果电脑上有其他设备，列表中的可能还有更多。可以依次选择每个设备，点击Connect、Calibration。如果能够正常进行校准，则表明这个设备对应电化学工作站。否则，可能会在点击Connect后弹出对话框报错，也可能点击校准没反应。
   
   一般情况下，如果始终插在同一个USB口，设备编号就不会再变动。因此在第一次尝试找到设备后，下次可以优先尝试连接相同编号的设备。
   
   点击Refresh，可以刷新串口设备的列表，需注意点击Refresh后连接会断开，需要重新选择设备并点击Connect连接。

![](C:\Users\Administrator\AppData\Roaming\marktext\images\2025-02-18-21-23-33-a4510414-cf98-4a8f-be0c-1eb0da10a534.png)

    在Range列表处，可以选择量程，提供了100 uA, 10 uA, 1 uA三个选项，实际分别能测到800 uA, 80 uA, 8 uA左右。超过最大电流时，度数会“饱和”在某个最大值处。

    默认量程为100 uA。手动更改量程时，会听到继电器切换声，说明更改成功。

6. 完成连接后，在Calibration区域，点击Start进行电压校准。
   
   注：每次机器启动时，第一次开始校准时如果发现卡顿，可以点击Stop后再Start一次，校准曲线就会流畅地显示出来。
   
   **校准时一定要连接电极，或将RE和CE连在一起。** 正常的校准图线是一条几乎没有波动的直线。**如果发现波动，往往是由于机器与电极没接牢。** 母头鳄鱼夹有时虽然看似能插紧，但实际连接不佳，可以使用双头鳄鱼夹代替。
   
   在观察到正常的校准图线后，可以随时点击Stop停止校准。也可以等待机器自动完成。随后会弹出对话框，点击确定，即可进行后续操作。
   
   ![](C:\Users\Administrator\AppData\Roaming\marktext\images\2025-02-18-21-31-44-60e96fe4-a425-422e-b166-2f2a8d7a91af.png)

7. 恒电位模式
   
   在Potentiostat区域，输入电压（对应WE相对于RE的电压），进入恒电位电解模式，同步显示I-t曲线。在曲线开始处，电流有一段快速上升和震荡区，这是由于存在双电层和机器电容的充电电流。可以随时点击Stop停止记录。点击Save，可以将图线保存成.txt文件。
   
   ![](C:\Users\Administrator\AppData\Roaming\marktext\images\2025-02-18-21-37-59-c62452f2-8fac-4e35-9c79-4ccacf53a605.png)

8. 循环伏安（CV）模式
   
   在Measurement区域可输入与CV相关的设置：最低电压（Start E， V），最高电压（End E, V），扫速（Scan rate, mV/s）。点击Start，机器会等待3 s，随后扫描曲线实时显示。横坐标表示WE相对于RE的电压。可以点击Stop停止，或点击Save保存。
   
   ![](C:\Users\Administrator\AppData\Roaming\marktext\images\2025-02-18-21-45-27-bf347d9a-1f2b-42c2-8e20-3bcda4c8d34d.png)

## 固件升级

SAKANA的控制软件和固件都可能会不断升级改进。控制软件很容易在电脑上安装，而当需要升级固件时，可采用如下方法：

1. 重置现有固件
   
   去掉CH340与电脑的连接，给机器断电。打开机器盒，按住树莓派pico上的白色BOOT按钮，同时将电源线USB接头接入电脑，随后松开按钮。在电脑上，会识别出一个名为RPI-RP2的磁盘。
   
   ![](C:\Users\Administrator\AppData\Roaming\marktext\images\2025-02-18-22-27-02-843fbba3-c0db-4528-9f57-aec28977de49.png)
   
   在https://micropython.org/download/rp2-pico/rp2-pico-latest.uf2下载.uf2文件，复制到RPI-RP2后，机器自动重启，此时现有固件程序被擦除。

2. 在电脑上安装Thonny并与设备连接，方法见[blog.csdn.net/zhuoqingjoking97298/article/details/114064833](https://blog.csdn.net/zhuoqingjoking97298/article/details/114064833)的第1~3部分。

3. 在Github主页的Sources文件夹中下载最新的.py文件，更名为main.py，用Thonny打开。将文件另存为到Raspberry Pi Pico中（相当于上述网页中操作的第5部分）。

4. 给Pico断电并重新通电，此时会自动执行最新版的程序，重新看到正常启动时的现象。

## 针对开发人员

如果你希望对SAKANA进行二次开发，以下内容可能有用。

### 串口通信格式

不使用控制软件时，也可以通过串口发送指令的方式控制SAKANA，波特率为115200。以下是当前支持的串口命令（#后的是注释）：

```
cali  #开始电压校准
stopcali #停止电压校准
stopmeas #停止CV测量
stopit #停止恒电位测量
par xxx yyy #设置电压校准参数，xxx和yyy分别是电压校准得到的斜率和截距
set xxx #开始恒电位模式，xxx是电压数值
measure v_start v_end scan_rate #开始CV测量
range 0 #设置量程为100 uA
range 1 #设置量程为10 uA
range 2 #设置量程为1 uAn
numcyc N #设置CV扫描圈数。N = 0时对应LSV。
```

串口返回值如下，所有返回信号都以$开头，以%结尾：

```
$fini% #校准/测量完成
$xxx yyy% #如果在校准模式，则xxx为MCP4725的输出电压，yyy为RE相对于地的电压
#如果在CV模式，则xxx和yyy分别为此时WE的电压和电流
$xxx% #恒电位模式下，以此格式返回当前时刻的电流值
```

### 电压/电流手动校准

在内置的固件程序中，已经对作为电压/电流测量模块的ADS1115和INA219进行了一般性的校准。如果你追求更高精度，可以针对每台机器分别进行校准。方法如下：

*电压校准*

1. 在Micropython固件程序中，修改ads_read()函数为：
   
   ```python
   def ads_read():
       value = ads.raw_to_v(ads.read_rev())
       return value
   ```

2. 将RE和CE连接，用高精度万用表或示波器连接RE和WE。在控制软件中，完成电压校准后，利用恒电位模式，输入电压数值，记录仪表读取到的实际电压数值。测定若干组后，进行线性回归，将斜率和截距替换进ads_read()中：

```python
def ads_read():
    value = ads.raw_to_v(ads.read_rev())
    #return value
    return value * 0.9284 - 0.0228 # fitted calibration function
```

*电流校准*

1. 修改read_i()函数中的correction_slope和correction_intercept分别为1.0和0.0：
   
   ```python
   def read_i():
       i = (ina.current()) / (resistor / 1011.6) * 1000
       correction_slope = 1.0
       correction_intercept = 0.0
       return (i - correction_intercept) / correction_slope # fitted calibration function for INA219
   ```

2. 将RE和CE连接，用一个精确已知阻值的电阻将CE和WE连接。在控制软件中，完成电压校准后，利用恒电位模式，输入电压数值，记录软件读取到的实际电流数值。测定若干组后，进行线性回归，将斜率和截距替换进read_i()中：
   
   ```python
   def read_i():
       i = (ina.current()) / (resistor / 1011.6) * 1000
       correction_slope = 1.0846
       correction_intercept = 0.2836
       return (i - correction_intercept) / correction_slope # fitted calibration function for INA219
   ```

最后，感谢你对本项目的支持。使用过程中如遇到问题，请向开发者反馈。我们所做的工作，将会为更普及、更便捷和更自由的科学研究贡献一份力量。
