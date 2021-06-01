: b32loop. ( u -- ) 0  <# 31 0 DO # 32 HOLD LOOP # #> TYPE ; 
: 1b. ( u -- ) cr \ 31-0, 1 bit groups generic legend
." 3|3|2|2|2|2|2|2|2|2|2|2|1|1|1|1|1|1|1|1|1|1|" cr
." 1|0|9|8|7|6|5|4|3|2|1|0|9|8|7|6|5|4|3|2|1|0|9|8|7|6|5|4|3|2|1|0 " cr
@ binary b32loop. decimal cr cr ;
: .. 1b. ;

\ \\\\\\\\\\\\\\\\\\\\\\\\\\\ F072 \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

$E000E100 constant NVIC ( Nested Vectored Interrupt  Controller ) 
NVIC $0 + constant NVIC_ISER ( Interrupt Set Enable Register ) 
: ISER_en ( INTn -- ) %1 swap lshift NVIC_ISER bis! ; \ Interrupt #N

: CAN_ien  30 ISER_en ; \ Interrupt #30
: TIM6_ien 17 ISER_en ; \ Interrupt #17 
\ : TIM14_ien 19 ISER_en ; \ Interrupt #19 
: TIM16_ien 21 ISER_en ; \ Interrupt #21
: EXTI_ien 7 ISER_en ;

$40021000 constant RCC ( Reset and clock control ) 
RCC $14 + constant AHBENR ( AHB Peripheral Clock enable register  AHBENR ) 
RCC $18 + constant APB2ENR ( APB2 peripheral clock enable register  APB2ENR ) 
RCC $1C + constant APB1ENR ( APB1 peripheral clock enable register  APB1ENR ) 

: GPIOA_cen   %1 17 lshift AHBENR bis! ;  \ AHBENR_IOPBEN    I/O port B clock enable
: GPIOB_cen   %1 18 lshift AHBENR bis! ;  \ AHBENR_IOPBEN    I/O port B clock enable
: GPIOC_cen   %1 19 lshift AHBENR bis! ;  \ AHBENR_IOPCEN    I/O port C clock enable
: CAN_cen   %1 25 lshift APB1ENR bis! ;  \ APB1ENR_CANEN    CAN interface clock enable
: TIM6_cen   %1 4 lshift APB1ENR bis! ;  \ TIM6_cen    Timer 6 clock enable
: TIM14_cen   %1 8 lshift APB1ENR bis! ;  \ APB1ENR_TIM14EN    Timer 14 clock enable
: TIM16_cen   %1 17 lshift APB2ENR bis! ;  \ APB2ENR_TIM16EN    TIM16 timer clock enable
: TIM17_cen   %1 18 lshift APB2ENR bis! ;  \ RCC_APB2ENR_TIM17EN    TIM17 timer clock enable
: SYSCFG_cen   %1 0 lshift APB2ENR bis! ;  \ RCC_APB2ENR_SYSCFGEN    SYSCFG clock enable


$40010400 constant EXTI ( External interrupt/event  controller ) 
EXTI $0 + constant EXTI_IMR ( Interrupt mask register  EXTI_IMR ) 
EXTI $8 + constant EXTI_RTSR ( Rising Trigger selection register  EXTI_RTSR ) 
EXTI $14 + constant EXTI_PR ( Pending register EXTI_PR ) 
: EXTI_IMR12   %1 12 lshift EXTI_IMR bis! ;
: EXTI_RT12   %1 12 lshift EXTI_RTSR bis! ;
: EXTI_PR12_clr   %1 12 lshift EXTI_PR bis! ;  


$40010000 constant SYSCFG ( System configuration controller ) 
SYSCFG $14 + constant SYSCFG_EXTICR4 ( external interrupt configuration register  4 ) 
SYSCFG $F8 + constant SYSCFG_L30
: L30_CAN_ien %1 1 lshift SYSCFG_L30 bis! ;
: EXTICR4_PB12  %0001 0 lshift SYSCFG_EXTICR4 bis! ;  \ SYSCFG_EXTICR4_EXTI12    EXTI 12 configuration bits


$40001000 constant TIM6 ( Basic-timers ) 
TIM6 $0 + constant TIM6_CR1 ( control register 1 ) 
\ TIM6 $4 + constant TIM6_CR2 ( control register 2 ) 
TIM6 $C + constant TIM6_DIER ( DMA/Interrupt enable register ) 
TIM6 $10 + constant TIM6_SR ( status register ) 
TIM6 $14 + constant TIM6_EGR ( event generation register ) 
TIM6 $24 + constant TIM6_CNT ( counter ) 
TIM6 $28 + constant TIM6_PSC ( prescaler ) 
TIM6 $2C + constant TIM6_ARR ( auto-reload register ) 
\ : TIM6_CR1_URS   %1 2 lshift TIM6_CR1 bis! ;  \ TIM6_CR1_URS    Update request source
\ : TIM6_CR1_UDIS   %1 1 lshift TIM6_CR1 bis! ;  \ TIM6_CR1_UDIS    Update disable
: TIM6_CR1_CEN   %1 0 lshift TIM6_CR1 bis! ;  \ TIM6_CR1_CEN    Counter enable
: TIM6_DIER_UIE   %1 0 lshift TIM6_DIER bis! ;  \ TIM6_DIER_UIE    Update interrupt enable
: TIM6_CNT@  TIM6_CNT @ ;  \ TIM6_CNT_CNT    Low counter value
: TIM6_PSC!   ( $XXXX -- ) TIM6_PSC ! ;  \ TIM6_PSC_PSC    Prescaler value
: TIM6_ARR!   ( $XXXX -- ) TIM6_ARR ! ;  \ TIM6_ARR_ARR    Low Auto-reload value
: TIM6_SR_UIF_clr   %1 0 lshift TIM6_SR bic! ;  \ TIM6_SR_UIF    Update interrupt flag
\ : TIM6_EGR_UG   %1 0 lshift TIM6_EGR bis! ;  \ TIM6_EGR_UG    Update generation


$40002000 constant TIM14 ( General-purpose-timers ) 
TIM14 $0 + constant TIM14_CR1 ( control register 1 ) 
\ TIM14 $C + constant TIM14_DIER ( DMA/Interrupt enable register ) 
TIM14 $24 + constant TIM14_CNT ( counter ) 
TIM14 $28 + constant TIM14_PSC ( prescaler ) 
TIM14 $2C + constant TIM14_ARR ( auto-reload register ) 
\ : TIM14_DIER_UIE   %1 0 lshift TIM14_DIER bis! ;  \ TIM14_DIER_UIE    Update interrupt enable
: TIM14_CR1_CEN   %1 0 lshift TIM14_CR1 bis! ;  \ TIM14_CR1_CEN    Counter enable


$40014400 constant TIM16 ( General-purpose-timers ) 
TIM16 $0 + constant TIM16_CR1 ( control register 1 ) 
\ TIM16 $4 + constant TIM16_CR2 ( control register 2 ) 
TIM16 $10 + constant TIM16_SR ( status register ) 
TIM16 $C + constant TIM16_DIER ( DMA/Interrupt enable register ) 
\ TIM16 $14 + constant TIM16_EGR ( event generation register ) 
\ TIM16 $24 + constant TIM16_CNT ( counter ) 
TIM16 $28 + constant TIM16_PSC ( prescaler ) 
TIM16 $2C + constant TIM16_ARR ( auto-reload register ) 
\ TIM16 $30 + constant TIM16_RCR ( repetition counter register ) 
\ TIM16 $34 + constant TIM16_CCR1 ( capture/compare register 1 ) 
: TIM16_CR1_CEN   %1 0 lshift TIM16_CR1 bis! ;  \ TIM16_CR1_CEN    Counter enable
: TIM16_SR_UIF_clr   %1 0 lshift TIM16_SR bic! ;  \ TIM16_SR_UIF    Update interrupt flag
: TIM16_DIER_UIE   %1 0 lshift TIM16_DIER bis! ;  \ TIM16_DIER_UIE    Update interrupt enable


$40014800 constant TIM17 ( General-purpose-timers ) 
TIM17 $0 + constant TIM17_CR1 ( control register 1 ) 
\ TIM17 $4 + constant TIM17_CR2 ( control register 2 ) 
TIM17 $10 + constant TIM17_SR ( status register ) 
TIM17 $14 + constant TIM17_EGR ( event generation register ) 
TIM17 $18 + constant TIM17_CCMR1
TIM17 $20 + constant TIM17_CCER ( capture/compare enable  register ) 
TIM17 $24 + constant TIM17_CNT ( counter ) 
TIM17 $28 + constant TIM17_PSC ( prescaler ) 
TIM17 $2C + constant TIM17_ARR ( auto-reload register ) 
TIM17 $34 + constant TIM17_CCR1 ( capture/compare register 1 ) 
TIM17 $44 + constant TIM17_BDTR ( break and dead-time register ) 
: TIM17_CR1_CEN   %1 0 lshift TIM17_CR1 bis! ;  \ TIM17_CR1_CEN    Counter enable


$48000000 constant GPIOA ( General-purpose I/Os ) 
GPIOA $0 + constant GPIOA_MODER ( read-write )  \ GPIO port mode register
GPIOA $C + constant GPIOA_PUPDR ( read-write )  \ GPIO port pull-up/pull-down  register
GPIOA $14 + constant GPIOA_ODR ( GPIO port output data register ) 
GPIOA $18 + constant GPIOA_BSRR ( GPIO port bit set/reset  register ) 
GPIOA $24 + constant GPIOA_AFRH ( read-write )  \ GPIO alternate function high  register
: GPIOA_22_PUPDR_pull %01 22 lshift GPIOA_PUPDR bis! ;
: GPIOA_11_12_MODER_AF %1010 22 lshift GPIOA_MODER bis! ;
: GPIOA_11_12_AFRH_AF4  %01000100 12 lshift GPIOA_AFRH bis! ; \ PA11 AF4 - CAN_RX, PA12 AF4 - CAN_TX

$48000400 constant GPIOB ( General-purpose I/Os ) 
GPIOB $0 + constant PB_MODER ( GPIO port mode register ) 
GPIOB $10 + constant GPIOB_IDR ( GPIO port input data register ) 
GPIOB $14 + constant GPIOB_ODR ( GPIO port output data register ) 
GPIOB $18 + constant GPIOB_BSRR ( GPIO port bit set/reset  register ) 
GPIOB $24 + constant GPIOB_AFRH ( GPIO alternate function high  register ) 
\ : PB_MODER9   ( %XX -- ) 18 lshift PB_MODER bis! ;  \ GPIOB_MODER_MODER9    Port x configuration bits y =  0..15
\ : GPIOB_AFRH_AFRH9   ( %XXXX -- ) 4 lshift GPIOB_AFRH bis! ;  \ GPIOB_AFRH_AFRH9    Alternate function selection for port x  bit y y = 8..15


$48000800 constant GPIOC ( General-purpose I/Os ) 
GPIOC $0 + constant GPIOC_MODER ( GPIO port mode register ) 
GPIOC $18 + constant GPIOC_BSRR ( GPIO port bit set/reset  register )
: GPIOC_MODER13   ( %XX -- ) 26 lshift GPIOC_MODER bis! ;
\ : GPIOC_ODR_ODR13   %1 13 lshift GPIOC_ODR bis! ;  \ GPIOC_ODR_ODR13    Port output data y =  0..15
: GPIOC_BSRR_BS13   %1 13 lshift GPIOC_BSRR bis! ;  \ GPIOC_BSRR_BS13    Port x set bit y  y=  0..15
: GPIOC_BSRR_BR13   %1 29 lshift GPIOC_BSRR bis! ;  \ GPIOC_BSRR_BR13    Port x reset bit y y =  0..15
 
$40006400 constant CAN ( Controller area network ) 
CAN $0   + constant CAN_MCR \ init
CAN $4   + constant CAN_MSR \ init
CAN $8   + constant CAN_TSR
CAN $C   + constant CAN_RF0R \ RX
CAN $10  + constant CAN_RF1R 
CAN $14  + constant CAN_IER \ RX, TX
CAN $18  + constant CAN_ESR 
CAN $1C  + constant CAN_BTR \ init
CAN $180 + constant CAN_TI0R \ TX
CAN $184 + constant CAN_TDT0R 
CAN $188 + constant CAN_TDL0R 
CAN $18C + constant CAN_TDH0R 
CAN $190 + constant CAN_TI1R 
CAN $194 + constant CAN_TDT1R 
CAN $198 + constant CAN_TDL1R 
CAN $19C + constant CAN_TDH1R 
CAN $1A0 + constant CAN_TI2R 
CAN $1A4 + constant CAN_TDT2R 
CAN $1A8 + constant CAN_TDL2R 
CAN $1AC + constant CAN_TDH2R 
CAN $1B0 + constant CAN_RI0R 
CAN $1B4 + constant CAN_RDT0R 
CAN $1B8 + constant CAN_RDL0R \ RX
CAN $1BC + constant CAN_RDH0R \ RX
CAN $1C0 + constant CAN_RI1R 
CAN $1C4 + constant CAN_RDT1R 
CAN $1C8 + constant CAN_RDL1R 
CAN $1CC + constant CAN_RDH1R 
CAN $200 + constant CAN_FMR \ init
CAN $204 + constant CAN_FM1R \ filter
CAN $20C + constant CAN_FS1R \ filter
CAN $214 + constant CAN_FFA1R \ filter
CAN $21C + constant CAN_FA1R \ filter
CAN $240 + constant CAN_F0R1 \ filter
CAN $244 + constant CAN_F0R2
CAN $248 + constant CAN_F1R1
CAN $24C + constant CAN_F1R2
CAN $250 + constant CAN_F2R1 ( Filter bank 2 register 1 ) 
: CAN_MCR_INRQ_set   %1 0 lshift CAN_MCR bis! ;
: CAN_MCR_INRQ_clr   %1 0 lshift CAN_MCR bic! ;
: CAN_MSR_INAK_test ( -- bit )  %1 0 lshift CAN_MSR bit@ ;  \ CAN1_MSR_INAK    INAK
: wait_for_set_inak ( -- bit ) BEGIN 57 drop CAN_MSR_INAK_test UNTIL ;
: wait_for_clr_inak ( -- bit ) BEGIN CAN_MSR_INAK_test WHILE 57 drop REPEAT ;
: CAN_MCR_SLAK_clr ( -- bit ) %1 1 lshift  CAN_MCR bic! ;
: CAN_FMR_FINIT_set ( -- bit )   %1 0 lshift CAN_FMR bis! ;  \ CAN1_FMR_FINIT    Filter init mode
: CAN_FMR_FINIT_clr ( -- bit )   %1 0 lshift CAN_FMR bic! ;  \ CAN1_FMR_FINIT    Filter init mode
: CAN_FA1R_FACT0_set ( -- bit )   %1 0 lshift CAN_FA1R bis! ;  \ CAN1_FA1R_FACT0    Filter active
: CAN_FA1R_FACT0_clr ( -- bit )   %1 0 lshift CAN_FA1R bic! ;  \ CAN1_FA1R_FACT0    Filter inactive
: CAN_FA1R_FACT_clr_all ( -- bit )   $3FFF CAN_FA1R bic! ;
: CAN_F0R1_ID_set ( ID -- ) $FFFF CAN_F0R1 bic! 	5 lshift  CAN_F0R1 bis! ; \ for id16
: CAN_FM1R_FBM0_idlist %1 0 lshift CAN_FM1R bic! ; \ Two 32-bit registers of filter bank 0 are in Identifier List mode.
: CAN_IER_FMPIE0_set ( -- ) %1 1 lshift CAN_IER bis! ;  \ FMPIE0 - FIFO0 IT enable
: CAN_RF0R_fifo0_release ( -- ) %1 5 lshift CAN_RF0R bis! ; \ RFOM0 set - Release FIFO 0 output mailbox

: CAN_TSR_RQCP0_test ( -- flag ) CAN_TSR @ %1 AND ;
: CAN_RF0R_FMP0_test ( -- flag ) CAN_RF0R @ %11 AND ;
: CAN_ESR_LEC_test ( -- flag ) CAN_ESR @ %1110000 AND ;

: CAN_IER_TMEIE_set  ( -- ) 1 0 lshift CAN_IER bis! ; \ CAN1_IER_TMEIE, TMEIE
: CAN_TI0R_TXRQ_req ( -- ) 1 0 lshift CAN_TI0R bis! ; \ CAN1_TI0R_TXRQ, TXRQ
: CAN_IER_ERRIE_set  ( -- ) 1 15 lshift CAN_IER bis! ; \ CAN1_IER_ERRIE, ERRIE
: CAN_IER_LECIE_set  ( -- ) 1 11 lshift CAN_IER bis! ; \ CAN1_IER_LECIE, LECIE
: CAN_TSR_RQCP0_clr ( -- ) 1 0 lshift CAN_TSR bis! ; \ set bit to clear flag !!!
: CAN_MSR_ERRI_clr ( -- ) 1 2 lshift CAN_MSR bis! ;
\ : CAN_MCR_DBF_clr ( -- bit ) %1 16 lshift  CAN_MCR bic! ;

1 variable verbose
0 variable rx_verbose

\ ====================== LED ==================================
0 variable led_state
: onn GPIOC_BSRR_BS13 ;
: off GPIOC_BSRR_BR13 ;
: toggle_led 1 led_state @ - dup led_state ! If onn ELSE off THEN ;

: led_init   GPIOC_cen   %01 GPIOC_MODER13 ( PC13 output ) ;


\ =============================================================
\ ======================= TIM16 1ms ===========================

0 variable delay_cnt
0 variable _ticker_head
_ticker_head variable _ticker_curr

\ ####  chain flag cnt arr proc  ####
: _ticker

	_ticker_curr @   \ curr handle

	BEGIN
		dup @  0<> \ handle valid ?
	WHILE
		dup cell+ @  0<> \ flag active ?
		IF
			dup  2 cells + @  \ cnt
			over 3 cells + @  \ arr
			= IF
				dup 2 cells + 0 swap ! \ reset cnt
				dup cell+ dup @ 2 = IF 0 swap ! ELSE drop THEN \ deactivate timeout
				dup 4 cells + @ execute
			ELSE
				dup 2 cells + 1 swap +! \ inc cnt
			THEN
		THEN
		@ \ next addr
	REPEAT
	drop ;

: tim16_1ms_irq
	dint
	delay_cnt dup @ 1- swap !
	_ticker
	TIM16_SR_UIF_clr
	eint ;

: delay ( ms -- ) 
	delay_cnt ! 
	BEGIN  delay_cnt @ 0 <=  UNTIL
;

: add_ticker ( proc ms -- handle )
	_ticker_curr @ \ keep prev ptr
	here  _ticker_curr !  \ new curr
	,   \ insert chain
	0 , \ init flag
	0 , \ init cnt
	,   \ insert ms from stack (arr)
	,   \ insert proc from stack
	_ticker_curr @ \ leave handle
;

: _ticker_set ( handle state -- )
	over cell+ ! \ set flag
	0 swap 2 cells + ! \ reset cnt
;

: ticker_start ( handle -- ) 1  _ticker_set ;
: ticker_stop  ( handle -- ) 0  _ticker_set ;
: ticker_time  ( handle -- ) 2  _ticker_set ;


: init_1ms 
	['] tim16_1ms_irq irq-tim16 !
	TIM16_cen
	7 TIM16_PSC ! \ 8/(7+1) = 1 MHz
	1000 TIM16_ARR ! \ 1 ms reload period 1000 ticks
	TIM16_DIER_UIE \ Interrupt enable
	TIM16_ien
	TIM16_CR1_CEN
;


\ =============================================================
\ ======================== LCD ================================

: LCD_GPIO_init
	GPIOA_cen   GPIOB_cen
	
	%11 18 lshift PB_MODER bic!   %10 18 lshift PB_MODER bis! \ Alternate function
	%0010 4 lshift GPIOB_AFRH bis!  \ PB9 AF2  -  TIM17_ch1
	
	%1111 PB_MODER bic!   %0101 PB_MODER bis!  \ PB0, PB1 output  -  RS, EN
	
	%11 2 lshift GPIOA_MODER bic!    	%01 2 lshift GPIOA_MODER bis!    \ PA1 output  -  LCD PWR
	
	%11111111 8 lshift GPIOA_MODER bic!    %01010101 8 lshift GPIOA_MODER bis!    \ PA7-PA4 output  -  LCD data
;

: LCD_on   %1 17 lshift GPIOA_BSRR bis! ;
: LCD_off  %1  1 lshift GPIOA_BSRR bis! ;

: LCD_TIM17_init
	TIM17_cen
	
	4 TIM17_PSC !
	256 TIM17_ARR !
	
	%111 4 lshift TIM17_CCMR1 bic!   %110 4 lshift TIM17_CCMR1 bis!   \  OCMODE PWM1
	
	130 TIM17_CCR1 !
	
	%1 15 lshift TIM17_BDTR  bis!  \ MOE
	%1 TIM17_CCER bis!
	TIM17_CR1_CEN
;

: rs1 %1  0 lshift GPIOB_BSRR bis! ; \  RS set - PB0
: rs0 %1 16 lshift GPIOB_BSRR bis! ; \ RS res - PB0

: en_str
	%1  1 lshift GPIOB_BSRR bis! \ EN on  - PB1 set
	20 0 do nop loop
	%1 17 lshift GPIOB_BSRR bis! ; \ EN off - PB1 res 

: LCD_WriteData ( dt -- )
	%1111 20 lshift  GPIOA_BSRR bis! \ clear
	%1111 AND
	4 lshift  GPIOA_BSRR bis! ;

: LCD_send
       dup 4 rshift LCD_WriteData   en_str
       LCD_WriteData   en_str ; 

: LCD_Command ( dt -- ) rs0  LCD_send ;

: LCD_Data ( dt -- ) rs1  LCD_send ;

: LCD_ini ( -- )
       40 delay   rs0
       3 LCD_WriteData    en_str   1 delay
       3 LCD_WriteData    en_str   1 delay
       3 LCD_WriteData    en_str   1 delay
       $20 LCD_Command    1 delay
       $20 LCD_Command    1 delay
       $0F LCD_Command    1 delay
       $01 LCD_Command    2 delay
       $06 LCD_Command    1 delay
       $02 LCD_Command    2 delay ;

: LCD_chr1 ( char place -- )          $80 OR LCD_Command   LCD_Data ;
: LCD_chr2 ( char place -- )  $40 +   $80 OR LCD_Command   LCD_Data ;

\ : LCD_str1 ( addr len -- )
\ 	8 0 DO
\ 		dup I > IF over I + c@ I LCD_chr1 ELSE $20 I LCD_chr1 THEN
\ 	LOOP
\ 	drop drop
\ ;

: init_LCD   LCD_GPIO_init   LCD_TIM17_init   LCD_ini ;


\ =============================================================
\ ======================= IrDA ================================

0 variable irda_buf  \ 4 bytes
0 variable irda_ind
0 variable irda_cnt
0 variable irda_bit
0 variable irda_timeout
0 variable lcd_state
0 variable h_lcd_dim

: irda_clr
	0 irda_buf !   \ irda_buf[0] = irda_buf[1] = irda_buf[2] = irda_buf[3] = 0;
	0 irda_ind !  0 irda_cnt !  0 irda_bit !   \ irda_ind = irda_cnt = irda_bit = 0;
;

: irda_EXTI_irq \ Rising trigger
	0 irda_timeout !

	irda_cnt @ dup 2 >=   swap 33 <=   irda_ind @ 4 <   AND AND
	IF
		irda_buf irda_ind @ +  dup c@ 1 rshift   swap c!

		TIM14_CNT @ 166 >
		IF
			irda_buf irda_ind @ +
			dup c@ $80 OR
			swap c!
		THEN
			
		0 TIM14_CNT !

		irda_bit @   dup 7 =   swap 1+ irda_bit !
		IF
			0 irda_bit !
			1 irda_ind +!
		THEN
	THEN
	
	1 irda_cnt +!
	EXTI_PR12_clr
;

: irda_EXTI_init
	EXTICR4_PB12
	EXTI_IMR12
	EXTI_RT12 
	['] irda_EXTI_irq irq-exti4_15 !
	EXTI_ien
;

0 variable mmask

: send_motor_cmd ( mask -- )
	8 lshift $03 OR CAN_TDL0R !
	\ $77665544 CAN_TDH0R !
	CAN_TI0R_TXRQ_req
;

: lcd_dim 
	0 lcd_state !
	LCD_off ;

: irda_msg
	h_lcd_dim @ ticker_time
	lcd_state dup @ 0= 
	IF 
		1 swap !
		LCD_on
	ELSE
		drop \ lcd_state
		1111 \ mark
		irda_buf 2+ c@
		CASE
			22 OF %00000   0 ENDOF \ 0
			12 OF %10000   1 ENDOF \ 1
			24 OF %00001   2 ENDOF \ 2
			94 OF %10100   3 ENDOF \ 3
			8 OF %10001   4 ENDOF \ 4
			28 OF %00011   5 ENDOF \ 5
			90 OF %01000   6 ENDOF \ 6
			66 OF %01010   7 ENDOF \ 7
			82 OF %11111   8 ENDOF \ 8
			74 OF %1000000 9 ENDOF \ 9
		ENDCASE
		dup 1111 = IF drop exit THEN \ no match
	
		.digit 0 LCD_chr2   $20 8 LCD_chr2
		dup mmask !
		send_motor_cmd
		drop \ mark
	THEN
;

: irda_tick
	irda_timeout @ 15 >   \  >15ms
	IF
		irda_clr
	ELSE
		irda_cnt @ 34 = 
		IF
			irda_buf 1+ c@   255 =
			irda_buf 2+ c@   irda_buf 3 + c@   +   255 =
			AND
			IF
				irda_msg  \  sendMsg( irda_buf[2] );
			THEN
			irda_clr
		ELSE
			1 irda_timeout +!
		THEN
	THEN
;

: tim14_IrDA_init 
	TIM14_cen
	79 TIM14_PSC ! \ 8M/(79+1) = 100kHz
	$FFFF TIM14_ARR ! \ max
	TIM14_CR1_CEN \ Counter enable
;

: init_IrDA
	\ GPIOB_cen
	SYSCFG_cen
	irda_EXTI_init
	tim14_IrDA_init
	
	['] irda_tick 1 add_ticker ticker_start
	
	['] lcd_dim 5000 add_ticker
		dup h_lcd_dim !
		ticker_time
;

\ =============================================================
\ =========================== CAN =============================

$001C0000 constant BTR   \ 8MHz 500Kbs CAN_BTR:  psc: 1,  seg1: 13,  seg2: 2

$70 constant ID_MOTOR
$71 constant ID_HEAT 
$73 constant ID_THROTTLE
$74 constant ID_ANEMO
$200 constant ID_REPLY

0 variable irq_cnt

: rx_handler 
	rx_verbose @ 
	IF
		irq_cnt @ . ."  FMI:" CAN_RDT0R @ 8 rshift $FF AND .
		
		CAN_RDL0R @ $01000A74 = 
		IF
			."  flow " CAN_RDH0R @ 16 rshift 256 swap - . cr
		ELSE
			." --- RX --- " CAN_RDH0R @ hex.  CAN_RDL0R @ hex. cr 
		THEN
	THEN
	CAN_RF0R_fifo0_release
;

: tx_handler
	verbose @ IF ." --- TX --- " irq_cnt @ . cr THEN
	CAN_TSR_RQCP0_clr
;

: err_handler
	verbose @ IF ." --- ERR --- " irq_cnt @ . CAN_ESR @ hex. cr THEN
	CAN_MSR_ERRI_clr
;
	
: CAN_handler
	\ dint
	1 irq_cnt +!
	CAN_TSR_RQCP0_test IF tx_handler THEN
	CAN_RF0R_FMP0_test IF rx_handler THEN  
	CAN_ESR_LEC_test  IF err_handler THEN
\ irq_cnt @ . cr
	\ eint
;

: tim_handler   toggle_led  TIM6_SR_UIF_clr ;


: init_CAN

	GPIOB_cen

	GPIOA_cen \ GPIOA clock enable
	GPIOA_22_PUPDR_pull
	GPIOA_11_12_MODER_AF
	GPIOA_11_12_AFRH_AF4 \ PA11 AF4 - CAN_RX, PA12 AF4 - CAN_TX
	
	CAN_cen
	
	CAN_MCR_INRQ_set	\ 1
	wait_for_set_inak	\ 2
	CAN_MCR_SLAK_clr	\ 3

	\ %1 30 lshift ( loopback mode )  BTR  OR  CAN_BTR ! \ 4
    BTR   CAN_BTR ! \ 4

	CAN_MCR_INRQ_clr	\ 5
	wait_for_clr_inak	\ 6
	CAN_FMR_FINIT_set	\ 7

	\ ----- RX filters ------
	CAN_FA1R_FACT_clr_all \ CAN_FA1R_FACT0_clr	\ 8
	$3FFF CAN_FS1R bic! \ 9 - all dual 16 bit
	$3FFF CAN_FM1R bis! \ all banks in IdList mode      \ CAN_FM1R_FBM0_idlist 	\ 9 - bank 0 in IdList mode
	$3FFF CAN_FFA1R bic! \ all filters 0 assigned to FIFO0
	
$FFFF  0 lshift  CAN_F0R1 bic!
 ID_MOTOR   5  0 + lshift  CAN_F0R1 bis!

\ $FFFF 16 lshift  CAN_F0R1 bic!
\ ID_REPLY   5 16 + lshift  CAN_F0R1 bis!
	
	CAN_FA1R_FACT0_set \ FACT0 activate
	
$FFFF CAN_F1R1 bic!
ID_REPLY  5 lshift CAN_F1R1 bis!   \ CAN_F1R1
%1 1 lshift CAN_FA1R bis!         \ FACT1 activate
	
$FFFF CAN_F2R1 bic!
 ID_HEAT 5 lshift CAN_F2R1 bis!   \ CAN_F2R1
%1 2 lshift CAN_FA1R bis!         \ FACT2 activate
	
	CAN_FMR_FINIT_clr		\ 10 - CAN1_FMR_FINIT_clr
	CAN_IER_FMPIE0_set 		\ 11 - FIFO0 IT enable

	
	\ ------------------- TX --------------------
	
	ID_MOTOR 21 lshift CAN_TI0R !  \ 
	
	$F CAN_TDT0R bic! \ clear before apply mask
	$8 CAN_TDT0R bis! \ DLC - data length
	
	
	CAN_IER_TMEIE_set		\ Interrupt generated when CAN1_TSR.RQCPx bit is set.


	\ ===================== errors =========================================
	
	CAN_IER_ERRIE_set
	CAN_IER_LECIE_set
	\ CAN_ESR_get_LEC
	
	\ ---------------- final -------------------------------------
	['] CAN_handler   irq-cec_can !
	
	L30_CAN_ien
	CAN_ien

	\ ['] tim_handler irq-tim6_dac !
	\ 
	\ TIM6_cen
	\ 1000 TIM6_PSC !
	\ 10000 TIM6_ARR !
	\ TIM6_ien
	\ TIM6_DIER_UIE
	\ TIM6_CR1_CEN
;

: init
	init_1ms
	init_IrDA
	init_LCD
	init_CAN
	
	%00001 send_motor_cmd
	0 verbose !
	0 rx_verbose !
	
	
$63 0 LCD_chr1
$75 1 LCD_chr1
$63 2 LCD_chr1
$61 3 LCD_chr1
$72 4 LCD_chr1
$65 5 LCD_chr1
$20 7 LCD_chr1

\ $35 0 LCD_chr2
\ $37 1 LCD_chr2
\ $20 7 LCD_chr2
;
init
