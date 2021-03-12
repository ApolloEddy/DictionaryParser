Public Structure Character
	Dim SelfChar As Char
	Dim AliasName As String
	Dim Phonetic As String
	Dim WordsList As List(Of Words)
	Dim Meanings As List(Of Meaning)

	Sub New(selfChar As Char)
		Me.SelfChar = selfChar
		WordsList = New List(Of Words)
		Meanings = New List(Of Meaning)
	End Sub
End Structure

Public Structure Words
	Dim SelfWords As String
	Dim WordsTypes As List(Of WordsType)
	Dim Phonetic As String
	Dim Meanings As List(Of Meaning)
	Sub New(selfWords As String)
		Me.SelfWords = selfWords
		WordsTypes = New List(Of WordsType)
		Meanings = New List(Of Meaning)
	End Sub
End Structure

Public Structure Meaning
	Dim Description As String
	Dim Example As List(Of String)
	Sub New(description As String)
		Me.Description = description
		Example = New List(Of String)
	End Sub
End Structure

Public Enum WordsType As Integer
	Noun ' 名词
	Verb ' 动词
	Adjective ' 形容词
	Adverb ' 副词
	Auxiliary ' 助词
	Interjection ' 叹词
	Quantifier ' 量词
	Written ' 书面语
	Dialect ' 方言
	Oral ' 口语
End Enum