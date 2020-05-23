create table Amplifiers
(
	Id_Amplifier int identity(1,1),

	Name_Amplifier varchar(100),

	BassKnob int,

	MidKnob int,

	TrebleKnob int


	Primary Key(Id_Amplifier)
)