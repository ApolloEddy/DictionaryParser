Imports System.Text
Imports System.Text.RegularExpressions

''' <summary>
''' 用于文本的分析、提取
''' </summary>
Public Class TextParser
	Public Property SourceString As String
	Public Property UsingString As String
	Public Property Pattern As String

	Public Sub New()
		UsingString = ""
		SourceString = ""
		Pattern = "(.+?)"
	End Sub
	Public Sub New(UsingString As String)
		Me.UsingString = UsingString
		SourceString = UsingString
		Pattern = "(.+?)"
	End Sub
	Public Sub New(UsingString As String, pattern As String)
		Me.UsingString = UsingString
		SourceString = UsingString
		Me.Pattern = pattern
	End Sub

	Public Overloads Shared Function RegParseOne(source As String, pattern As String) As String
		Dim reg As New Regex(pattern)
		Return reg.Match(source).Value
	End Function
	Public Overloads Function RegParseOne() As String
		Return RegParseOne(UsingString, Pattern)
	End Function
	Public Overloads Shared Function RegParse(source As String, pattern As String) As List(Of String)
		Dim reg As New Regex(pattern)
		Dim rlist As New List(Of String)
		For Each match As Match In reg.Matches(source)
			rlist.Add(match.Value)
		Next
		Return rlist
	End Function
	Public Overloads Function RegParse() As List(Of String)
		Return RegParse(UsingString, Pattern)
	End Function
	Public Overloads Shared Function ExtractOne(source As String, beginS As String, endS As String) As String
		Dim reg As New Regex($"{beginS}(?<ret>(.+?)){endS}")
		Dim ret = reg.Match(source).Groups("ret").Value
		Return ret
	End Function
	Public Overloads Function ExtractOne(beginS As String, endS As String) As String
		Return ExtractOne(UsingString, beginS, endS)
	End Function
	Public Overloads Shared Function Extract(source As String, beginS As String, endS As String) As List(Of String)
		Dim reg As New Regex($"{beginS}(?<ret>(.+?)){endS}")
		Dim rlist As New List(Of String)
		Dim cache As String
		For Each match As Match In reg.Matches(source)
			cache = match.Groups("ret").Value
			rlist.Add(cache)
		Next
		Return rlist
	End Function
	Public Overloads Function Extract(beginS As String, endS As String) As List(Of String)
		Return Extract(UsingString, beginS, endS)
	End Function
	Public Overloads Shared Function ExtrimStrings(input As String, ParamArray values As String())
		Dim sb As New StringBuilder(input)
		For Each v As String In values
			sb.Replace(v, "")
		Next
		Return sb.ToString()
	End Function
	Public Overloads Function RemoveStrings(ParamArray ByVal items As String()) As String
		Return RemoveStrings(SourceString, items)
	End Function
	Public Overloads Shared Function RemoveStrings(input As String, ParamArray ByVal items As String()) As String
		Dim sb As New StringBuilder(input)
		For Each str As String In items
			sb.Replace(str, "")
		Next
		Return sb.ToString()
	End Function
End Class
