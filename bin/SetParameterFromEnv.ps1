#$input = "D:\...\input.xml"

#$output = "D:\...\output.xml"

param (
	[string]$inputFile = $(throw "-inputFile is required."),
	[string]$outputFile = $(throw "-outputFile is required.")
)

$inputDoc = New-Object System.Xml.XmlDocument
$inputDoc.Load($inputFile)

$inputDoc.SelectNodes("//setParameter") `
	| %{ 
		$parameterName = $_.GetAttribute("name") -replace "[^\w]", ""

		if (Test-Path env:$parameterName) {
			$environmentParameter = Get-Item env:$parameterName
			Write-Host "* Updating $parameterName value to ""$($environmentParameter.Value)"""
			$_.SetAttribute("value", $environmentParameter.Value)
		}
	}

$inputDoc.Save($outputFile)