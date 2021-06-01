@
@    Mecrisp-Stellaris - A native code Forth implementation for ARM-Cortex M microcontrollers
@    Copyright (C) 2013  Matthias Koch
@
@    This program is free software: you can redistribute it and/or modify
@    it under the terms of the GNU General Public License as published by
@    the Free Software Foundation, either version 3 of the License, or
@    (at your option) any later version.
@
@    This program is distributed in the hope that it will be useful,
@    but WITHOUT ANY WARRANTY; without even the implied warranty of
@    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
@    GNU General Public License for more details.
@
@    You should have received a copy of the GNU General Public License
@    along with this program.  If not, see <http://www.gnu.org/licenses/>.
@

@ Routinen für die Interrupthandler, die zur Laufzeit neu gesetzt werden können.
@ Code for interrupt handlers that are exchangeable on the fly

@------------------------------------------------------------------------------
@ Alle Interrupthandler funktionieren gleich und werden komfortabel mit einem Makro erzeugt:
@ All interrupt handlers work the same way and are generated with a macro:
@------------------------------------------------------------------------------

@ interrupt wwdg
@ interrupt pvd_vddio2
interrupt rtc
@ interrupt flash
@ interrupt rcc_crs
@ interrupt exti0_1
@ interrupt exti2_3
interrupt exti4_15
@ interrupt tsc
@ interrupt dma_ch1
interrupt dma_23_dma2_12
@ interrupt dma_4567_dma2_345
interrupt adc
@ interrupt tim1_up
@ interrupt tim1_cc
interrupt tim2
@ interrupt tim3
interrupt tim6_dac
interrupt tim7 
interrupt tim14
interrupt tim15
interrupt tim16
@ interrupt tim17
interrupt i2c1
@ interrupt i2c2
interrupt spi1
@ interrupt spi2
interrupt usart1
@ interrupt usart2
interrupt usart3
interrupt cec_can
@ interrupt usb

@------------------------------------------------------------------------------

