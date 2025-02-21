# SAKANA Electrochemical Workstation User Manual

## Package Contents

- Machine box (Dimensions: approximately 11 cm * 11 cm * 5 cm)
- 3 female alligator clips
- 1 serial-to-USB adapter (CH340), pre-connected to the machine.
- 1 Type-C or Micropython power cable (hereinafter referred to as the power cable)

Two serial cables, pre-connected to the CH340, extend from the front side of the machine box, the same side as the power cable connector.

On the other side of the machine box are 3 electrode wires corresponding to the Reference Electrode (RE), Working Electrode (WE), and Counter Electrode (CE), respectively. These can be connected to the electrodes using the alligator clips.

## Typical Parameters

Voltage Range: -2.7 V ~ +2.2 V
Voltage Error Range: ±30 mV
Maximum Current: ~800 uA
Current Error Range: <±1 uA

## Usage Instructions

1. Install the CH340 driver on your computer. Download link: [www.wch.cn/download/CH341SER_EXE.html](https://www.wch.cn/download/CH341SER_EXE.html)

2. Download the latest control software on your computer. Software download link: [github.com/RimoAccelerator/SAKANA/releases/tag/Windows](https://github.com/RimoAccelerator/SAKANA/releases/tag/Windows)
   
   **Note:**
   
   This software requires .NET Framework 8 or later.
   
   If you are using a laptop, please connect it to a power source for smooth software operation.

3. Plug the CH340 into a USB port on your computer. The red LED on the CH340 should turn on. If you are using a USB hub, be aware that some hubs can be loose, so try different ports if necessary.
   
   ***Connecting the CH340 to the machine:*** If the connection between the machine and CH340 is unfortunately broken, connect the two serial cables to the RX and TX pins of the CH340, respectively. If the wires are not marked, you can distinguish them as follows: Open the top cover of the machine box. The wire marked RX on the PCB connects to the TX pin of the CH340; the wire marked TX on the PCB connects to the RX pin of the CH340.  You can also connect them randomly. If the connection is incorrect, the software will recognize the device after clicking "Connect," but there will be no response during calibration or measurement. In this case, simply swap the two wires.

4. Power the machine using the power cable. The power supply voltage is 5 V. You can plug it into a computer's USB port or a mobile phone USB charger.  The machine is pre-programmed with Micropython code. After powering on, you will hear a "click" sound, which is the normal sound of the relay activating. If you open the box, you will see the Raspberry Pi Pico's yellow LED on and two of the four relays lit with blue LEDs, indicating normal startup.

5. Run the control software. The COM list displays all connected serial port devices. If the power cable is also connected to the computer, there will be at least two devices: the power cable and the CH340. If there are other devices connected to your computer, there may be more in the list.  You can select each device one by one and click "Connect" and "Calibration." If the calibration proceeds normally, this device corresponds to the electrochemical workstation. Otherwise, an error dialog box may pop up after clicking "Connect," or there may be no response after clicking "Calibration."
   
   Generally, if the device is always plugged into the same USB port, the device number will not change. Therefore, after finding the device for the first time, you can prioritize connecting to the same numbered device next time.
   
   Clicking "Refresh" will refresh the list of serial port devices. Note that clicking "Refresh" will disconnect the current connection, requiring you to re-select the device and click "Connect."
   
   In the "Range" list, you can select the current range.  The options are 100 uA, 10 uA, and 1 uA, which can actually measure up to approximately 800 uA, 80 uA, and 8 uA, respectively.  When the current exceeds the maximum, the reading will "saturate" at a maximum value.
   
   The default range is 100 uA. When manually changing the range, you will hear a relay switching sound, indicating a successful change.

6. After connecting, click "Start" in the "Calibration" area to perform voltage calibration.
   
   **Note:**  Every time the machine starts, if you experience lag during the first calibration, you can click "Stop" and then "Start" again. The calibration curve will then display smoothly.
   
   **It is crucial to connect the electrodes or connect RE and CE together during calibration.** A normal calibration curve is a nearly straight line with minimal fluctuations. **If fluctuations are observed, it is often due to a poor connection between the machine and the electrodes.** Female alligator clips sometimes appear to be securely attached but may have a poor connection. Double-ended alligator clips can be used as an alternative.
   
   After observing a normal calibration curve, you can click "Stop" to stop the calibration at any time. You can also wait for the machine to complete automatically. A dialog box will then pop up; click "OK" to proceed.

7. Potentiostat Mode
   
   In the "Potentiostat" area, enter the voltage (corresponding to the voltage of WE relative to RE) to enter the constant potential electrolysis mode. The I-t curve will be displayed synchronously. At the beginning of the curve, there is a rapid rise and oscillation in the current due to the charging current of the double layer and the machine capacitance. You can click "Stop" to stop recording at any time. Click "Save" to save the curve as a .txt file.

8. Cyclic Voltammetry (CV) Mode
   
   In the "Measurement" area, you can enter CV-related settings: minimum voltage (Start E, V), maximum voltage (End E, V), and scan rate (Scan rate, mV/s). Click "Start." The machine will wait for 3 seconds, and then the scanned curve will be displayed in real-time. The horizontal axis represents the voltage of WE relative to RE. You can click "Stop" to stop or "Save" to save the data.

## Firmware Upgrade

The SAKANA control software and firmware may be continuously upgraded and improved. The control software is easy to install on a computer. When a firmware upgrade is required, use the following method:

1. Reset the existing firmware
   
   Disconnect the CH340 from the computer and power off the machine. Open the machine box, press and hold the white BOOT button on the Raspberry Pi Pico, and simultaneously plug the USB connector of the power cable into the computer. Then release the button. The computer will recognize a disk named RPI-RP2.
   
   Download the .uf2 file from [https://micropython.org/download/rp2-pico/rp2-pico-latest.uf2](https://micropython.org/download/rp2-pico/rp2-pico-latest.uf2), copy it to RPI-RP2. The machine will automatically restart, and the existing firmware will be erased.

2. Install Thonny on your computer and connect to the device. 

3. Download the latest .py file from the "Sources" folder on the Github homepage, rename it to "main.py," and open it with Thonny. Save the file to the Raspberry Pi Pico.

4. Power off and then power on the Pico. The latest version of the program will automatically execute, and you will see the normal startup sequence again.

## For Developers

If you wish to further develop SAKANA, the following information may be helpful.

### Serial Communication Format

You can also control SAKANA by sending commands through the serial port without using the control software. The baud rate is 115200. The following serial commands are currently supported (# denotes a comment):

```
cali #Start voltage calibration  
stopcali #Stop voltage calibration  
stopmeas #Stop CV measurement  
stopit #Stop constant potential measurement  
par xxx yyy #Set voltage calibration parameters, xxx and yyy are the slope and intercept obtained from voltage calibration  
set xxx #Start constant potential mode, xxx is the voltage value  
measure v_start v_end scan_rate #Start CV measurement  
range 0 #Set range to 100 uA  
range 1 #Set range to 10 uA  
range 2 #Set range to 1 uA  
numcyc N #Set the number of CV scan cycles. N = 0 corresponds to LSV.
```

Serial return values are as follows. All return signals start with $ and end with %:

```
$fini% #Calibration/measurement complete
$xxx yyy% #If in calibration mode, xxx is the output voltage of MCP4725, yyy is the voltage of RE relative to ground
#If in CV mode, xxx and yyy are the voltage and current of WE, respectively
$xxx% #In constant potential mode, the current value at the current time is returned in this format
```

### Manual Voltage/Current Calibration

The built-in firmware has a general calibration for the ADS1115 and INA219, which are used as voltage/current measurement modules. If you require higher accuracy, you can calibrate each machine individually. The methods are as follows:

*Voltage Calibration*

1. In the Micropython firmware, modify the `ads_read()` function to:
   
       def ads_read():
           value = ads.raw_to_v(ads.read_rev())
           return value

2. Connect RE and CE, and connect a high-precision multimeter or oscilloscope between RE and WE. In the control software, after completing the voltage calibration, use the constant potential mode, input a voltage value, and record the actual voltage value read by the instrument. After measuring several sets of data, perform linear regression, and substitute the slope and intercept into `ads_read()`:
   
   ```
   def ads_read():
       value = ads.raw_to_v(ads.read_rev())
       #return value
       return value * 0.9284 - 0.0228 # fitted calibration function
   ```
   
   *Current Calibration*
   
   1. Modify the `correction_slope` and `correction_intercept` in the `read_i()` function to 1.0 and 0.0, respectively:
      
      ```
      def read_i():
          i = (ina.current()) / (resistor / 1011.6) * 1000
          correction_slope = 1.0
          correction_intercept = 0.0
          return (i - correction_intercept) / correction_slope # fitted calibration function for INA219
      ```
      
      
   2. Connect RE and CE, and connect a resistor with a precisely known resistance value between CE and WE. In the control software, after completing the voltage calibration, use the constant potential mode, input a voltage value, and record the actual current value read by the software. After measuring several sets of data, perform linear regression, and substitute the slope and intercept into `read_i()`:
      
      ```
      def read_i():
          i = (ina.current()) / (resistor / 1011.6) * 1000
          correction_slope = 1.0846
          correction_intercept = 0.2836
          return (i - correction_intercept) / correction_slope # fitted calibration function for INA219
      ```
      
      Finally, thank you for your support of this project. If you encounter any problems during use, please provide feedback to the developers. Our work will contribute to more accessible, convenient, and free scientific research.