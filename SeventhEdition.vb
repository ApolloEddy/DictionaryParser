Imports System.Text
Imports System.Text.RegularExpressions
Imports System.IO

Public Class SeventhEdition : Inherits AllEditions
	Public Sub New(textPath As String)
		MyBase.New(textPath)
	End Sub

	Public Function ParseLineToWords(input As String) As Words ', currentChar As Character
		Dim tp As New TextParser(input)
		Dim headString As String = tp.ExtractOne("【", "】")
		Dim words As New Words(headString)
		With words
			.Phonetic = ExtractPhonetic(input)
			.WordsTypes = ExtractWordsTypes(input)
			.Meanings = ExtractMeanings(input)
		End With
		Return words
	End Function

	' 提取拼音
	Public Function ExtractPhonetic(input As String) As String
		Dim phonetic As New StringBuilder()
		For Each ch As Char In input
			If AllCasesPY.Contains(ch) Then phonetic.Append(ch)
			If Not phonetic.Length = 0 And Not AllCasesPY.Contains(ch) Then Exit For
		Next
		Return phonetic.ToString()
	End Function
	' 提取词语类型
	Public Function ExtractWordsTypes(input As String) As List(Of WordsType)
		Dim wts As New List(Of WordsType)
		For Each ch As Char In New TextParser(input).Extract("〈", "〉")
			Select Case ch
				Case "名" : wts.Add(WordsType.Noun)
				Case "动" : wts.Add(WordsType.Verb)
				Case "形" : wts.Add(WordsType.Adjective)
				Case "副" : wts.Add(WordsType.Adverb)
				Case "助" : wts.Add(WordsType.Auxiliary)
				Case "叹" : wts.Add(WordsType.Interjection)
				Case "量" : wts.Add(WordsType.Quantifier)
				Case "书" : wts.Add(WordsType.Written)
				Case "方" : wts.Add(WordsType.Dialect)
				Case "口" : wts.Add(WordsType.Oral)
			End Select
		Next
		Return wts
	End Function
	' 提取字、词释义
	Protected MeaningExtractReg As New Regex("^(([a-z])|ā|á|ǎ|à|ō|ó|ǒ|ò|ē|é|ě|è|ī|í|ǐ|ì|ū|ú|ǔ|ù|ü|ǖ|ǘ|ǚ|ǜ|ê|ê̄|ế|ê̌|ề|m̄|ḿ|m̀|ń|ň|ǹ|ẑ|ĉ|ŝ|ŋ|Ā|Á|Ǎ|À|Ō|Ó|Ǒ|Ò|Ē|É|Ě|È|Ī|Í|Ǐ|Ì|Ū|Ú|Ǔ|Ù|Ü|Ǖ|Ǘ|Ǚ|Ǜ|Ê|Ê̄|Ế|Ê̌|Ề|M̄|Ḿ|M̀|Ń|Ň|Ǹ|Ẑ|Ĉ|Ŝ|Ŋ|❶|。|！|？|）|〉|》|；)(?<ret>(.*)(。|）|！|？|"”|…|[a-z]|｜|\||\>|〉))")
	Protected MeaningDescriptionExtractReg As New Regex("([\u4e00-\u9fa5]|\(|（|《|〈|"“)(.+?)(：|。|！|？|）|〉|》|；)")
	Protected MeaningExampleExtractReg As New Regex("：(.*)(。|！|？|）|…|〉|；)")
	Public Function ExtractMeanings(input As String) As List(Of Meaning)
		Dim cache As String = ""
		Dim meaningPart As Boolean = False
		Dim mainPart = MeaningExtractReg.Match(input).Groups("ret").Value
		Dim meaning As Meaning
		Dim ret As New List(Of Meaning)
		For Each ch As Char In mainPart.ToString()
			If CircleNumbers.Contains(ch) Then
				If meaningPart Then
					meaning = GetMeaning(cache)
					cache = ""
					ret.Add(meaning)
					Continue For
				End If
				meaningPart = True
				cache = ""
				Continue For
			Else
				If meaningPart Then cache += ch : Continue For
			End If
			If Not AllCasesPY.Contains(ch) Then cache += ch
		Next
		If ret.Count = 0 Then
			meaning = GetMeaning(cache)
			ret.Add(meaning)
		End If
		Return ret
	End Function
	Private Function GetMeaning(cache As String) As Meaning
		For Each str As String In TextParser.RegParse(cache, "【(.+?)】")
			cache.Replace(str, "")
		Next
		Dim meaning As New Meaning((TextParser.RemoveStrings(MeaningDescriptionExtractReg.Match(cache).Value, "：", “！”, "？", "。", "〉", "；")))
		For Each match As Match In MeaningExampleExtractReg.Matches(cache)
			For Each exp As String In TextParser.RemoveStrings(match.Value, "<", "《", "：", "〈").Split("｜")
				meaning.Example.Add(exp)
			Next
		Next
		Return meaning
	End Function

End Class
