Module Process

	Sub Main()
		'On Error Resume Next
		Dim ed7 As New SeventhEdition("D:\VS2019\VBDemo\DictionaryParser\现代汉语词典 第七版.txt")
		For Each line As String In ed7.SourceContent.ToString().Split(vbLf)
			Try
				Dim wd As Words = ed7.ParseLineToWords(line)
				Console.WriteLine($"【{wd.SelfWords}】 {wd.Phonetic}   [{wd.WordsTypes(0)}]")
				For i As Integer = 0 To wd.Meanings.Count - 1
					Console.WriteLine($"   {wd.Meanings(i).Description}:")
					For j As Integer = 0 To wd.Meanings(i).Example.Count - 1
						Console.WriteLine($"      {wd.Meanings(i).Example(j)}")
					Next
				Next
				Console.WriteLine("")
			Catch ex As Exception
				'Throw New Exception(ex.Message)
				'Console.WriteLine(ex.Message)
			End Try
		Next
		Console.ReadKey()
	End Sub

End Module
