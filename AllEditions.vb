Imports System.Text
Imports System.Text.RegularExpressions
Imports System.IO

Public MustInherit Class AllEditions
	Public Property TextPath As String
	Public ReadOnly Property LCasePY As String = "āáǎàōóǒòēéěèīíǐìūúǔùüǖǘǚǜêê̄ếê̌ềm̄ḿm̀ńňǹẑĉŝŋ"
	Public ReadOnly Property UCasePY As String = "ĀÁǍÀŌÓǑÒĒÉĚÈĪÍǏÌŪÚǓÙÜǕǗǙǛÊÊ̄ẾÊ̌ỀM̄ḾM̀ŃŇǸẐĈŜŊ"
	Public ReadOnly Property AllCasesPY As String = "āáǎàōóǒòēéěèīíǐìūúǔùüǖǘǚǜêê̄ếê̌ềm̄ḿm̀ńňǹẑĉŝŋĀÁǍÀŌÓǑÒĒÉĚÈĪÍǏÌŪÚǓÙÜǕǗǙǛÊÊ̄ẾÊ̌ỀM̄ḾM̀ŃŇǸẐĈŜŊabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
	Public ReadOnly Property CircleNumbers As String = "❶❷❸❹❺❻❼❽❾❿"
	Public ReadOnly Property SignTab As String = ""
	Public ReadOnly Property SourceContent As StringBuilder
		Get
			Return New StringBuilder(File.ReadAllText(TextPath))
		End Get
	End Property

	Public Sub New(textPath As String)
		Me.TextPath = textPath
	End Sub

End Class
