# https://github.com/chrisb2/pyb_ina219
"""MicroPython library for the INA219 sensor.
This library supports the INA219 sensor from Texas Instruments with
MicroPython using the I2C bus.
"""
#import logging
import utime
from math import trunc
from micropython import const

# https://github.com/chrisb2/pyb_ina219
class INA219:
    """Provides all the functionality to interact with the INA219 sensor."""

    RANGE_16V = const(0)  # Range 0-16 volts
    RANGE_32V = const(1)  # Range 0-32 volts

    GAIN_1_40MV = const(0)  # Maximum shunt voltage 40mV
    GAIN_2_80MV = const(1)  # Maximum shunt voltage 80mV
    GAIN_4_160MV = const(2)  # Maximum shunt voltage 160mV
    GAIN_8_320MV = const(3)  # Maximum shunt voltage 320mV
    GAIN_AUTO = const(-1)  # Determine gain automatically

    ADC_9BIT = const(0)  # 9-bit conversion time  84us.
    ADC_10BIT = const(1)  # 10-bit conversion time 148us.
    ADC_11BIT = const(2)  # 11-bit conversion time 2766us.
    ADC_12BIT = const(3)  # 12-bit conversion time 532us.
    ADC_2SAMP = const(9)  # 2 samples at 12-bit, conversion time 1.06ms.
    ADC_4SAMP = const(10)  # 4 samples at 12-bit, conversion time 2.13ms.
    ADC_8SAMP = const(11)  # 8 samples at 12-bit, conversion time 4.26ms.
    ADC_16SAMP = const(12)  # 16 samples at 12-bit,conversion time 8.51ms
    ADC_32SAMP = const(13)  # 32 samples at 12-bit, conversion time 17.02ms.
    ADC_64SAMP = const(14)  # 64 samples at 12-bit, conversion time 34.05ms.
    ADC_128SAMP = const(15)  # 128 samples at 12-bit, conversion time 68.10ms.

    __ADC_CONVERSION = {
        ADC_9BIT: "9-bit",
        ADC_10BIT: "10-bit",
        ADC_11BIT: "11-bit",
        ADC_12BIT: "12-bit",
        ADC_2SAMP: "12-bit, 2 samples",
        ADC_4SAMP: "12-bit, 4 samples",
        ADC_8SAMP: "12-bit, 8 samples",
        ADC_16SAMP: "12-bit, 16 samples",
        ADC_32SAMP: "12-bit, 32 samples",
        ADC_64SAMP: "12-bit, 64 samples",
        ADC_128SAMP: "12-bit, 128 samples"
    }

    __ADDRESS = 0x40

    __REG_CONFIG = 0x00
    __REG_SHUNTVOLTAGE = 0x01
    __REG_BUSVOLTAGE = 0x02
    __REG_POWER = 0x03
    __REG_CURRENT = 0x04
    __REG_CALIBRATION = 0x05

    __RST = 15
    __BRNG = 13
    __PG1 = 12
    __PG0 = 11
    __BADC4 = 10
    __BADC3 = 9
    __BADC2 = 8
    __BADC1 = 7
    __SADC4 = 6
    __SADC3 = 5
    __SADC2 = 4
    __SADC1 = 3
    __MODE3 = 2
    __MODE2 = 1
    __MODE1 = 0

    __OVF = 1
    __CNVR = 2

    __BUS_RANGE = [16, 32]
    __GAIN_VOLTS = [0.04, 0.08, 0.16, 0.32]

    __CONT_SH_BUS = 7

    __AMP_ERR_MSG = ('Expected current %.3fA is greater '
                     'than max possible current %.3fA')
    __RNG_ERR_MSG = ('Expected amps %.2fA, out of range, use a lower '
                     'value shunt resistor')
    __VOLT_ERR_MSG = ('Invalid voltage range, must be one of: '
                      'RANGE_16V, RANGE_32V')

    __LOG_FORMAT = '%(asctime)s - %(levelname)s - INA219 %(message)s'
    __LOG_MSG_1 = ('shunt ohms: %.3f, bus max volts: %d, '
                   'shunt volts max: %.2f%s, '
                   'bus ADC: %s, shunt ADC: %s')
    __LOG_MSG_2 = ('calibrate called with: bus max volts: %dV, '
                   'max shunt volts: %.2fV%s')
    __LOG_MSG_3 = ('Current overflow detected - '
                   'attempting to increase gain')

    __SHUNT_MILLIVOLTS_LSB = 0.01  # 10uV
    __BUS_MILLIVOLTS_LSB = 4  # 4mV
    __CALIBRATION_FACTOR = 0.04096
    # Max value supported value (65534 decimal) of the calibration register
    # (D0 bit is always zero, p31 of spec)
    __MAX_CALIBRATION_VALUE = 0xFFFE
    # In the spec (p17) the current LSB factor for the minimum LSB is
    # documented as 32767, but a larger value (100.1% of 32767) is used
    # to guarantee that current overflow can always be detected.
    __CURRENT_LSB_FACTOR = 32800

    def __init__(self, shunt_ohms, i2c, max_expected_amps=None,
                 address=__ADDRESS):
        """Construct the class.
        At a minimum pass in the resistance of the shunt resistor and I2C
        interface to which the sensor is connected.
        Arguments:
        shunt_ohms -- value of shunt resistor in Ohms (mandatory).
        i2c -- an instance of the I2C class from the *machine* module, either
            I2C(1) or I2C(2) (mandatory).
        max_expected_amps -- the maximum expected current in Amps (optional).
        address -- the I2C address of the INA219, defaults to
            *0x40* (optional).
        log_level -- set to logging.DEBUG to see detailed calibration
            calculations (optional).
        """
        self._i2c = i2c
        self._address = address
        self._shunt_ohms = shunt_ohms
        self._max_expected_amps = max_expected_amps
        self._min_device_current_lsb = self._calculate_min_current_lsb()
        self._gain = None
        self._auto_gain_enabled = False

    def configure(self, voltage_range=RANGE_32V, gain=GAIN_AUTO,
                  bus_adc=ADC_12BIT, shunt_adc=ADC_12BIT):
        """Configure and calibrate how the INA219 will take measurements.
        Arguments:
        voltage_range -- The full scale voltage range, this is either 16V
            or 32V represented by one of the following constants;
            RANGE_16V, RANGE_32V (default).
        gain -- The gain which controls the maximum range of the shunt
            voltage represented by one of the following constants;
            GAIN_1_40MV, GAIN_2_80MV, GAIN_4_160MV,
            GAIN_8_320MV, GAIN_AUTO (default).
        bus_adc -- The bus ADC resolution (9, 10, 11, or 12-bit) or
            set the number of samples used when averaging results
            represent by one of the following constants; ADC_9BIT,
            ADC_10BIT, ADC_11BIT, ADC_12BIT (default),
            ADC_2SAMP, ADC_4SAMP, ADC_8SAMP, ADC_16SAMP,
            ADC_32SAMP, ADC_64SAMP, ADC_128SAMP
        shunt_adc -- The shunt ADC resolution (9, 10, 11, or 12-bit) or
            set the number of samples used when averaging results
            represent by one of the following constants; ADC_9BIT,
            ADC_10BIT, ADC_11BIT, ADC_12BIT (default),
            ADC_2SAMP, ADC_4SAMP, ADC_8SAMP, ADC_16SAMP,
            ADC_32SAMP, ADC_64SAMP, ADC_128SAMP
        """
        self.__validate_voltage_range(voltage_range)
        self._voltage_range = voltage_range

        if self._max_expected_amps is not None:
            if gain == self.GAIN_AUTO:
                self._auto_gain_enabled = True
                self._gain = self._determine_gain(self._max_expected_amps)
            else:
                self._gain = gain
        else:
            if gain != self.GAIN_AUTO:
                self._gain = gain
            else:
                self._auto_gain_enabled = True
                self._gain = self.GAIN_1_40MV

        self._calibrate(
            self.__BUS_RANGE[voltage_range], self.__GAIN_VOLTS[self._gain],
            self._max_expected_amps)
        self._configure(voltage_range, self._gain, bus_adc, shunt_adc)

    def voltage(self):
        """Return the bus voltage in volts."""
        value = self._voltage_register()
        return float(value) * self.__BUS_MILLIVOLTS_LSB / 1000

    def supply_voltage(self):
        """Return the bus supply voltage in volts.
        This is the sum of the bus voltage and shunt voltage. A
        DeviceRangeError exception is thrown if current overflow occurs.
        """
        return self.voltage() + (float(self.shunt_voltage()) / 1000)

    def current(self):
        """Return the bus current in milliamps.
        A DeviceRangeError exception is thrown if current overflow occurs.
        """
        self._handle_current_overflow()
        return self._current_register() * self._current_lsb * 1000

    def power(self):
        """Return the bus power consumption in milliwatts.
        A DeviceRangeError exception is thrown if current overflow occurs.
        """
        self._handle_current_overflow()
        return self._power_register() * self._power_lsb * 1000

    def shunt_voltage(self):
        """Return the shunt voltage in millivolts.
        A DeviceRangeError exception is thrown if current overflow occurs.
        """
        self._handle_current_overflow()
        return self._shunt_voltage_register() * self.__SHUNT_MILLIVOLTS_LSB

    def sleep(self):
        """Put the INA219 into power down mode."""
        configuration = self._read_configuration()
        self._configuration_register(configuration & 0xFFF8)

#    def wake(self):
#        """Wake the INA219 from power down mode."""
#        configuration = self._read_configuration()
#        self._configuration_register(configuration | 0x0007)
#        # 40us delay to recover from powerdown (p14 of spec)
#        utime.sleep_us(40)
        

    def current_overflow(self):
        """Return true if the sensor has detect current overflow.
        In this case the current and power values are invalid.
        """
        return self._has_current_overflow()

    def reset(self):
        """Reset the INA219 to its default configuration."""
        self._configuration_register(1 << self.__RST)

    def _handle_current_overflow(self):
        if self._auto_gain_enabled:
            while self._has_current_overflow():
                self._increase_gain()
        else:
            if self._has_current_overflow():
                raise DeviceRangeError(self.__GAIN_VOLTS[self._gain])

    def _determine_gain(self, max_expected_amps):
        shunt_v = max_expected_amps * self._shunt_ohms
        if shunt_v > self.__GAIN_VOLTS[3]:
            raise ValueError(self.__RNG_ERR_MSG % max_expected_amps)
        gain = min(v for v in self.__GAIN_VOLTS if v > shunt_v)
        return self.__GAIN_VOLTS.index(gain)

#    def _increase_gain(self):
#        gain = self._read_gain()
#        if gain < len(self.__GAIN_VOLTS) - 1:
#            gain = gain + 1
#            self._calibrate(self.__BUS_RANGE[self._voltage_range],
#                            self.__GAIN_VOLTS[gain])
#            self._configure_gain(gain)
#            # 1ms delay required for new configuration to take effect,
#            # otherwise invalid current/power readings can occur.
#            utime.sleep_ms(1)
#        else:
#            raise DeviceRangeError(self.__GAIN_VOLTS[gain], True)

    def _configure(self, voltage_range, gain, bus_adc, shunt_adc):
        configuration = (
            voltage_range << self.__BRNG | gain << self.__PG0 |
            bus_adc << self.__BADC1 | shunt_adc << self.__SADC1 |
            self.__CONT_SH_BUS)
        self._configuration_register(configuration)

    def _calibrate(self, bus_volts_max, shunt_volts_max,
                   max_expected_amps=None):

        max_possible_amps = shunt_volts_max / self._shunt_ohms

        self._current_lsb = \
            self._determine_current_lsb(max_expected_amps, max_possible_amps)

        self._power_lsb = self._current_lsb * 20

        max_current = self._current_lsb * 32767

        max_shunt_voltage = max_current * self._shunt_ohms

        calibration = trunc(self.__CALIBRATION_FACTOR /
                            (self._current_lsb * self._shunt_ohms))
        self._calibration_register(calibration)

    def _determine_current_lsb(self, max_expected_amps, max_possible_amps):
        if max_expected_amps is not None:
            if max_expected_amps > round(max_possible_amps, 3):
                raise ValueError(self.__AMP_ERR_MSG %
                                 (max_expected_amps, max_possible_amps))
            if max_expected_amps < max_possible_amps:
                current_lsb = max_expected_amps / self.__CURRENT_LSB_FACTOR
            else:
                current_lsb = max_possible_amps / self.__CURRENT_LSB_FACTOR
        else:
            current_lsb = max_possible_amps / self.__CURRENT_LSB_FACTOR

        if current_lsb < self._min_device_current_lsb:
            current_lsb = self._min_device_current_lsb
        return current_lsb

    def _configuration_register(self, register_value):
        self.__write_register(self.__REG_CONFIG, register_value)

    def _read_configuration(self):
        return self.__read_register(self.__REG_CONFIG)

    def _calculate_min_current_lsb(self):
        return self.__CALIBRATION_FACTOR / \
            (self._shunt_ohms * self.__MAX_CALIBRATION_VALUE)

    def _read_gain(self):
        configuration = self._read_configuration()
        gain = (configuration & 0x1800) >> self.__PG0
        return gain

    def _configure_gain(self, gain):
        configuration = self._read_configuration()
        configuration = configuration & 0xE7FF
        self._configuration_register(configuration | (gain << self.__PG0))
        self._gain = gain

    def _calibration_register(self, register_value):
        self.__write_register(self.__REG_CALIBRATION, register_value)

    def _has_current_overflow(self):
        ovf = self._read_voltage_register() & self.__OVF
        return (ovf == 1)

    def _voltage_register(self):
        register_value = self._read_voltage_register()
        return register_value >> 3

    def _read_voltage_register(self):
        return self.__read_register(self.__REG_BUSVOLTAGE)

    def _current_register(self):
        return self.__read_register(self.__REG_CURRENT, True)

    def _shunt_voltage_register(self):
        return self.__read_register(self.__REG_SHUNTVOLTAGE, True)

    def _power_register(self):
        return self.__read_register(self.__REG_POWER)

    def __validate_voltage_range(self, voltage_range):
        if voltage_range > len(self.__BUS_RANGE) - 1:
            raise ValueError(self.__VOLT_ERR_MSG)

    def __write_register(self, register, register_value):
        register_bytes = self.__to_bytes(register_value)
        self._i2c.writeto_mem(self._address, register, register_bytes)

    def __to_bytes(self, register_value):
        return bytearray([(register_value >> 8) & 0xFF, register_value & 0xFF])

    def __read_register(self, register, negative_value_supported=False):
        register_bytes = self._i2c.readfrom_mem(self._address, register, 2)
        register_value = int.from_bytes(register_bytes, 'big')
        if negative_value_supported:
            # Two's compliment
            if register_value > 32767:
                register_value -= 65536
        return register_value


    def __max_expected_amps_to_string(self, max_expected_amps):
        if max_expected_amps is None:
            return ''
        else:
            return ', max expected amps: %.3fA' % max_expected_amps


class DeviceRangeError(Exception):
    """This exception is throw to prevent invalid readings.
    Invalid readings occur When the current is greater than allowed given
    calibration of the device.
    """

    __DEV_RNG_ERR = ('Current out of range (overflow), '
                     'for gain %.2fV')

    def __init__(self, gain_volts, device_max=False):
        """Construct the class."""
        msg = self.__DEV_RNG_ERR % gain_volts
        if device_max:
            msg = msg + ', device limit reached'
        super(DeviceRangeError, self).__init__(msg)
        self.gain_volts = gain_volts
        self.device_limit_reached = device_max

import utime as time

_REGISTER_MASK = const(0x03)
_REGISTER_CONVERT = const(0x00)
_REGISTER_CONFIG = const(0x01)
_REGISTER_LOWTHRESH = const(0x02)
_REGISTER_HITHRESH = const(0x03)

_OS_MASK = const(0x8000)
_OS_SINGLE = const(0x8000)  # Write: Set to start a single-conversion
_OS_BUSY = const(0x0000)  # Read: Bit=0 when conversion is in progress
_OS_NOTBUSY = const(0x8000)  # Read: Bit=1 when no conversion is in progress

_MUX_MASK = const(0x7000)
_MUX_DIFF_0_1 = const(0x0000)  # Differential P  =  AIN0, N  =  AIN1 (default)
_MUX_DIFF_0_3 = const(0x1000)  # Differential P  =  AIN0, N  =  AIN3
_MUX_DIFF_1_3 = const(0x2000)  # Differential P  =  AIN1, N  =  AIN3
_MUX_DIFF_2_3 = const(0x3000)  # Differential P  =  AIN2, N  =  AIN3
_MUX_SINGLE_0 = const(0x4000)  # Single-ended AIN0
_MUX_SINGLE_1 = const(0x5000)  # Single-ended AIN1
_MUX_SINGLE_2 = const(0x6000)  # Single-ended AIN2
_MUX_SINGLE_3 = const(0x7000)  # Single-ended AIN3

_PGA_MASK = const(0x0E00)
_PGA_6_144V = const(0x0000)  # +/-6.144V range  =  Gain 2/3
_PGA_4_096V = const(0x0200)  # +/-4.096V range  =  Gain 1
_PGA_2_048V = const(0x0400)  # +/-2.048V range  =  Gain 2 (default)
_PGA_1_024V = const(0x0600)  # +/-1.024V range  =  Gain 4
_PGA_0_512V = const(0x0800)  # +/-0.512V range  =  Gain 8
_PGA_0_256V = const(0x0A00)  # +/-0.256V range  =  Gain 16

_MODE_MASK = const(0x0100)
_MODE_CONTIN = const(0x0000)  # Continuous conversion mode
_MODE_SINGLE = const(0x0100)  # Power-down single-shot mode (default)

_DR_MASK = const(0x00E0)     # Values ADS1015/ADS1115
_DR_128SPS = const(0x0000)   # 128 /8 samples per second
_DR_250SPS = const(0x0020)   # 250 /16 samples per second
_DR_490SPS = const(0x0040)   # 490 /32 samples per second
_DR_920SPS = const(0x0060)   # 920 /64 samples per second
_DR_1600SPS = const(0x0080)  # 1600/128 samples per second (default)
_DR_2400SPS = const(0x00A0)  # 2400/250 samples per second
_DR_3300SPS = const(0x00C0)  # 3300/475 samples per second
_DR_860SPS = const(0x00E0)  # -   /860 samples per Second

_CMODE_MASK = const(0x0010)
_CMODE_TRAD = const(0x0000)  # Traditional comparator with hysteresis (default)
_CMODE_WINDOW = const(0x0010)  # Window comparator

_CPOL_MASK = const(0x0008)
_CPOL_ACTVLOW = const(0x0000)  # ALERT/RDY pin is low when active (default)
_CPOL_ACTVHI = const(0x0008)  # ALERT/RDY pin is high when active

_CLAT_MASK = const(0x0004)  # Determines if ALERT/RDY pin latches once asserted
_CLAT_NONLAT = const(0x0000)  # Non-latching comparator (default)
_CLAT_LATCH = const(0x0004)  # Latching comparator

_CQUE_MASK = const(0x0003)
_CQUE_1CONV = const(0x0000)  # Assert ALERT/RDY after one conversions
_CQUE_2CONV = const(0x0001)  # Assert ALERT/RDY after two conversions
_CQUE_4CONV = const(0x0002)  # Assert ALERT/RDY after four conversions
# Disable the comparator and put ALERT/RDY in high state (default)
_CQUE_NONE = const(0x0003)

_GAINS = (
    _PGA_6_144V,  # 2/3x
    _PGA_4_096V,  # 1x
    _PGA_2_048V,  # 2x
    _PGA_1_024V,  # 4x
    _PGA_0_512V,  # 8x
    _PGA_0_256V   # 16x
)

_GAINS_V = (
    6.144,  # 2/3x
    4.096,  # 1x
    2.048,  # 2x
    1.024,  # 4x
    0.512,  # 8x
    0.256  # 16x
)

_CHANNELS = {
    (0, None): _MUX_SINGLE_0,
    (1, None): _MUX_SINGLE_1,
    (2, None): _MUX_SINGLE_2,
    (3, None): _MUX_SINGLE_3,
    (0, 1): _MUX_DIFF_0_1,
    (0, 3): _MUX_DIFF_0_3,
    (1, 3): _MUX_DIFF_1_3,
    (2, 3): _MUX_DIFF_2_3,
}

_RATES = (
    _DR_128SPS,   # 128/8 samples per second
    _DR_250SPS,   # 250/16 samples per second
    _DR_490SPS,   # 490/32 samples per second
    _DR_920SPS,   # 920/64 samples per second
    _DR_1600SPS,  # 1600/128 samples per second (default)
    _DR_2400SPS,  # 2400/250 samples per second
    _DR_3300SPS,  # 3300/475 samples per second
    _DR_860SPS    # - /860 samples per Second
)


class ADS1115:
    def __init__(self, i2c, address=0x48, gain=1):
        self.i2c = i2c
        self.address = address
        self.gain = gain
        self.temp2 = bytearray(2)

    def _write_register(self, register, value):
        self.temp2[0] = value >> 8
        self.temp2[1] = value & 0xff
        self.i2c.writeto_mem(self.address, register, self.temp2)

    def _read_register(self, register):
        self.i2c.readfrom_mem_into(self.address, register, self.temp2)
        return (self.temp2[0] << 8) | self.temp2[1]

    def raw_to_v(self, raw):
        v_p_b = _GAINS_V[self.gain] / 32767
        return raw * v_p_b

    def set_conv(self, rate=4, channel1=0, channel2=None):
        """Set mode for read_rev"""
        self.mode = (_CQUE_NONE | _CLAT_NONLAT |
                     _CPOL_ACTVLOW | _CMODE_TRAD | _RATES[rate] |
                     _MODE_SINGLE | _OS_SINGLE | _GAINS[self.gain] |
                     _CHANNELS[(channel1, channel2)])

    def read(self, rate=4, channel1=0, channel2=None):
        """Read voltage between a channel and GND.
           Time depends on conversion rate."""
        self._write_register(_REGISTER_CONFIG, (_CQUE_NONE | _CLAT_NONLAT |
                             _CPOL_ACTVLOW | _CMODE_TRAD | _RATES[rate] |
                             _MODE_SINGLE | _OS_SINGLE | _GAINS[self.gain] |
                             _CHANNELS[(channel1, channel2)]))
        while not self._read_register(_REGISTER_CONFIG) & _OS_NOTBUSY:
            time.sleep_ms(1)
        res = self._read_register(_REGISTER_CONVERT)
        return res if res < 32768 else res - 65536

    def read_rev(self):
        """Read voltage between a channel and GND. and then start
           the next conversion."""
        res = self._read_register(_REGISTER_CONVERT)
        self._write_register(_REGISTER_CONFIG, self.mode)
        return res if res < 32768 else res - 65536

    def alert_start(self, rate=4, channel1=0, channel2=None,
                    threshold_high=0x4000, threshold_low=0, latched=False) :
        """Start continuous measurement, set ALERT pin on threshold."""
        self._write_register(_REGISTER_LOWTHRESH, threshold_low)
        self._write_register(_REGISTER_HITHRESH, threshold_high)
        self._write_register(_REGISTER_CONFIG, _CQUE_1CONV |
                             _CLAT_LATCH if latched else _CLAT_NONLAT |
                             _CPOL_ACTVLOW | _CMODE_TRAD | _RATES[rate] |
                             _MODE_CONTIN | _GAINS[self.gain] |
                             _CHANNELS[(channel1, channel2)])

    def conversion_start(self, rate=4, channel1=0, channel2=None):
        """Start continuous measurement, trigger on ALERT/RDY pin."""
        self._write_register(_REGISTER_LOWTHRESH, 0)
        self._write_register(_REGISTER_HITHRESH, 0x8000)
        self._write_register(_REGISTER_CONFIG, _CQUE_1CONV | _CLAT_NONLAT |
                             _CPOL_ACTVLOW | _CMODE_TRAD | _RATES[rate] |
                             _MODE_CONTIN | _GAINS[self.gain] |
                             _CHANNELS[(channel1, channel2)])

    def alert_read(self):
        """Get the last reading from the continuous measurement."""
        res = self._read_register(_REGISTER_CONVERT)
        return res if res < 32768 else res - 65536

class mcp4725:
    def __init__(self, channel, address):
        self.channel = channel
        self.address = address
    
    def mcp4725_init(self, scl_pin, sda_pin):
        self.i2c = machine.I2C(self.channel, scl=machine.Pin(scl_pin), sda=machine.Pin(sda_pin))

    def mcp4725_send(self, value):
        buf=bytearray(2)
        buf[0]=(value >> 8) & 0xFF
        buf[1]=value & 0xFF
        self.i2c.writeto(self.address, buf)

import machine, utime
from machine import UART, Pin, I2C
import uasyncio as asyncio

# UART and I2C settings
uart = UART(0, baudrate=115200, tx=Pin(12), rx=Pin(13), bits=8, stop=1)
i2c_channel = 1
scl_pin = 19
sda_pin = 18
address = 0x60

# Pin settings
led = machine.Pin(25, machine.Pin.OUT)
led.value(1)
v_samp_switch = machine.Pin(1, machine.Pin.OUT)
i_samp_switch = machine.Pin(2, machine.Pin.OUT)
range_1_switch = machine.Pin(3, machine.Pin.OUT)
range_1_switch.value(1)
range_2_switch = machine.Pin(4, machine.Pin.OUT)
range_2_switch.value(1)

# Calibration and CV measurement settings
IS_CALIBRATION = False
CAL_SLOPE = -1.080
CAL_INTERCEPT = 2.711
IS_CV = False
IS_I_T = False
MAX_CYCLE = 0.5 # number of CV cycles * 0.5; 0 for LSV

#DPV-related parameters
IS_DPV = False
DPV_VSTART = 0
DPV_VEND = 1
DPV_VINCRE = 0.004 #V
DPV_EPUL = 0.05 #V
DPV_PULWIDTH = 0.05 #s
DPV_SAMPWIDTH = 0.017 #s
DPV_PERIOD = 0.5 #s
DPV_STARTTIME = 0 #ms
DPV_NSTEP = 0

# Initialize I2C and sensors
i2c = I2C(i2c_channel, scl=Pin(scl_pin), sda=Pin(sda_pin))
ina = INA219(10, i2c)
ina.configure(shunt_adc=ina.ADC_12BIT, gain=ina.GAIN_2_80MV)
ads = ADS1115(i2c, 72, 1)
ads.set_conv(7)

# Voltage settings
v_start = -1.0
v_end = 1.0
scan_rate = 200.0
v_incre = 0.005
num_cycle = 0.0
ticktime = v_incre / (scan_rate / 1000)
this_duty = v_start
N = 8.0
resistor = 6126

def writeVoltage(v_out):
    if IS_CALIBRATION:
        voltage0_out = v_out
    else:
        voltage0_out = (v_out - CAL_INTERCEPT) / CAL_SLOPE
    
    dac0_value = int(voltage0_out / 4.5 * 4095)
    dac0 = mcp4725(i2c_channel, address)
    dac0.mcp4725_init(scl_pin, sda_pin)
    dac0.mcp4725_send(dac0_value)

def ads_read():
    value = ads.raw_to_v(ads.read_rev())
    #return value
    #return value * 0.9284 - 0.0228 # fitted calibration function
    return value * 0.9125 - 0.0115

def handle_uart_commands(buf):
    global this_duty, num_cycle, count, IS_CALIBRATION, IS_CV, resistor
    global v_incre, v_start, v_end, scan_rate, N, ticktime
    global CAL_SLOPE, CAL_INTERCEPT, MAX_CYCLE
    global IS_I_T, IS_DPV
    global DPV_PERIOD, DPV_STARTTIME, IS_DPV, DPV_NSTEP, DPV_VSTART, DPV_VEND, DPV_VINCRE, DPV_EPUL, DPV_PULWIDTH, DPV_SAMPWIDTH
    if buf == 'cali':
        print('cali received')
        IS_CALIBRATION = True
        IS_CV = False
        IS_I_T = False
        v_start = 1.5
        v_end = 0.5
        ticktime = v_incre * 1000 / scan_rate
        this_duty = v_start
        scan_rate = 200.0
        v_incre = 0.002
        num_cycle = -0.5
        writeVoltage(this_duty)
        v_samp_switch.value(1)
        i_samp_switch.value(1)
        utime.sleep(1)
    elif buf == 'stopcali':
        print('calibration stopped')
        IS_CALIBRATION = False
    elif buf == 'stopmeas':
        print('measurement stopped')
        IS_CV = False
    elif buf == 'stopit':
        print('i_t stopped')
        IS_I_T = False
    elif 'par ' in buf:
        print(buf)
        CAL_SLOPE, CAL_INTERCEPT = [float(i) for i in buf.split(' ')[1:]]
    elif 'numcyc ' in buf:
        print(buf)
        MAX_CYCLE = float(buf.split(' ')[1]) * 0.5
    elif 'meas' in buf:
        IS_CV = True
        IS_CALIBRATION = False
        IS_I_T = False
        v_start, v_end, scan_rate = [float(i) for i in buf.split(' ')[1:]]
        v_incre = - scan_rate * 0.000025 # if v_incre is too small, the scan rate will be significantly lower than expected
        v_start = -v_start
        v_end = -v_end
        ticktime = v_incre * 1000 / scan_rate
        if v_start < v_end:
            v_incre = -v_incre #assume that v_incre is always negative. For negative direction, v_incre should be positive
        this_duty = v_start
        num_cycle = 0
        writeVoltage(this_duty)
        v_samp_switch.value(0)
        i_samp_switch.value(1)
        print(f'measurement started: {v_start} {v_end} {scan_rate}')
        utime.sleep(3)
    elif 'stopdpv' in buf:
        IS_DPV = False
        print('dpv stopped')
    elif 'dpv' in buf:
        IS_DPV = True
        IS_CALIBRATION = False
        IS_CV = False
        DPV_VSTART, DPV_VEND, DPV_VINCRE, DPV_EPUL, DPV_PULWIDTH, DPV_SAMPWIDTH, DPV_PERIOD = [float(i) for i in buf.split(' ')[1:]]
        DPV_VSTART = -DPV_VSTART
        DPV_VEND = -DPV_VEND
        DPV_VINCRE = abs(DPV_VINCRE)
        DPV_EPUL = -DPV_EPUL
        if DPV_VSTART > DPV_VEND:
            DPV_VINCRE = -DPV_VINCRE
        DPV_STARTTIME = utime.ticks_ms()
        DPV_NSTEP = 0
        this_duty = DPV_VSTART
        writeVoltage(this_duty)
        v_samp_switch.value(0)
        i_samp_switch.value(1)
        print(f'dpv started: {DPV_VSTART} {DPV_VEND} {DPV_VINCRE} {DPV_EPUL} {DPV_PULWIDTH} {DPV_SAMPWIDTH} {DPV_PERIOD}')
        utime.sleep(2)
    elif 'set' in buf:
        _, volt = buf.split(' ')
        print(f'set volt to {volt}')
        v_samp_switch.value(0)
        i_samp_switch.value(1)
        IS_I_T = True
        writeVoltage(-float(volt))
    elif 'range' in buf:
        _, range = buf.split(' ')
        if range == '0': # 100 uA range, use the 6k8 resistor
            range_1_switch.value(1)
            range_2_switch.value(1)
            resistor = 6126
            print('set range to 6k8')
        elif range == '1': # 10 uA range, use the 68k resistor
            range_1_switch.value(1)
            range_2_switch.value(0)
            resistor = 61800
            print('set range to 68k')
        if range == '2': # 1 uA range, use the 680k resistor
            range_1_switch.value(0)
            range_2_switch.value(0)
            resistor = 680000
            print('set range to 680k')

def read_i():
    i = (ina.current()) / (resistor / 1011.6) * 1000
    correction_slope = 1.0846
    correction_intercept = 0.2836
    return (i - correction_intercept) / correction_slope # fitted calibration function for INA219

DPV_I_BEFORE = []
DPV_I_AFTER = []
def main_loop():
    global this_duty, num_cycle, count, IS_CALIBRATION, IS_CV
    global v_incre, v_start, v_end, scan_rate, N, ticktime
    global CAL_SLOPE, CAL_INTERCEPT
    global DPV_STARTTIME, IS_DPV, DPV_NSTEP, DPV_I_BEFORE, DPV_I_AFTER
    temp = []
    temp_v = []
    temp_it = []
    count = 0

    writeVoltage(this_duty)
    while True:
        if uart.any():
            buf = uart.read().decode().strip()
            handle_uart_commands(buf)
            continue
        if not (IS_CALIBRATION or IS_CV or IS_I_T or IS_DPV):
            utime.sleep(0.001)
            continue

        if IS_I_T:
            i = read_i()
            temp_it.append(i)
            if len(temp_it) > N:
                i_avg = sum(temp_it) / N
                temp_it = []
                uart.write(f'${i_avg:.4f}%') 
            utime.sleep(0.002)
            continue
        if IS_DPV:
            utime.sleep(0.001)
            elapsed_time = utime.ticks_diff(utime.ticks_ms(), DPV_STARTTIME) / 1000
            period_num = int(elapsed_time / DPV_PERIOD)
            period_time = elapsed_time - DPV_PERIOD * period_num
            #print(f'DPV_PERIOD - DPV_PULWIDTH - DPV_SAMPWIDTH {DPV_PERIOD - DPV_PULWIDTH - DPV_SAMPWIDTH} DPV_PERIOD - DPV_PULWIDTH {DPV_PERIOD - DPV_PULWIDTH}')
            #print(f'elapsed_time {elapsed_time} period_num {period_num} period_time {period_time}')
            if period_num > DPV_NSTEP: # at the beginning of each period (>1), set voltage and calculate dI for the previous period
                DPV_NSTEP = period_num
                this_duty = DPV_VSTART + DPV_VINCRE * DPV_NSTEP
                if (DPV_VINCRE > 0 and this_duty > DPV_VEND) or (DPV_VINCRE < 0 and this_duty < DPV_VEND):
                    print('DPV fini')
                    uart.write('$fini%')
                    IS_DPV = False
                else:
                    writeVoltage(this_duty)
                if len(DPV_I_BEFORE) > 0 and len(DPV_I_AFTER) > 0:
                    i_before_avg = sum(DPV_I_BEFORE) / len(DPV_I_BEFORE)
                    i_after_avg = sum(DPV_I_AFTER) / len(DPV_I_AFTER)
                    delta_i = i_after_avg - i_before_avg
                    print(f'${this_duty:.4f} {delta_i:.4f}%')
                    uart.write(f'${-this_duty:.4f} {delta_i:.4f}%')
                    DPV_I_AFTER = []
                    DPV_I_BEFORE = []
            if period_time > DPV_PERIOD - DPV_PULWIDTH - DPV_SAMPWIDTH and period_time < DPV_PERIOD - DPV_PULWIDTH: #start I-sampling
                DPV_I_BEFORE.append(read_i())
            elif period_time > DPV_PERIOD - DPV_PULWIDTH: #add pulse
                this_duty = DPV_VSTART + DPV_VINCRE * DPV_NSTEP + DPV_EPUL
                writeVoltage(this_duty)
                if period_time > DPV_PERIOD - DPV_SAMPWIDTH: #start I-sampling
                    DPV_I_AFTER.append(read_i())
            continue
            #this_duty += DPV_VINCRE

        utime.sleep_us(int(ticktime / N * 1000000))
        if v_start > v_end:
            if this_duty < v_end:
                this_duty = v_end
                v_incre *= -1
                num_cycle += 0.5
            elif this_duty > v_start:
                this_duty = v_start
                v_incre *= -1
                num_cycle += 0.5
        else:
            if this_duty > v_end:
                this_duty = v_end
                v_incre *= -1
                num_cycle += 0.5
            elif this_duty < v_start:
                this_duty = v_start
                v_incre *= -1
                num_cycle += 0.5

        if num_cycle > MAX_CYCLE:
            print('fini')
            utime.sleep(0.1)
            uart.write('$fini%')
            IS_CALIBRATION = False
            IS_CV = False
            num_cycle = 0

        count += 1

        if IS_CALIBRATION:
            v_act = ads_read()
            temp_v.append(v_act)
        elif IS_CV:
            i = read_i()
            temp.append(i)

        this_duty += v_incre / N
        if count > N:
            count = 0
            writeVoltage(this_duty)
            if IS_CALIBRATION:
                v_avg = sum(temp_v) / N
                uart.write(f'${this_duty:.4f} {v_avg:.4f}%')
                #print(f'{this_duty:.4f} {v_avg:.4f}')
            else:
                i_avg = sum(temp) / N
                #print(i_avg)
                uart.write(f'${-this_duty:.4f} {i_avg:.4f}%') # i versus E
            temp = []
            temp_v = []
            

if __name__ == "__main__":
    main_loop()
