# SteamTradingCardOverview

Dear Steam trading card collectors! This is my tool to generate an overview of how many Steam trading cards are there available in your account and how much are they currently worth.

Two usage modes are available, one just for an overview of the cards and the second shows their potential sale value:  

## Extract

This mode reads the content from your Steam Badges page from the standard input and generates a comma separated list of games and number of remaining steam trading cards. 

First you need to go to your Steam client, and open the Badges page (hover over your profile name in the STORE LIBRARY COMMUNITY YOURNAME and click Badges). Then copy all of the text (<kbd>Ctrl</kbd><kbd>A</kbd> <kbd>Ctrl</kbd><kbd>C</kbd>) and paste it into a text file.
Note: If you have a lot of games with cards, you might need to paste the content from all pages of the Badges page.

Then you can run the tool as
 
    steamtradingcardoverview extract <pasted.txt >export.csv

This generates a CSV file - sample output:

    "game","card drops remaining"
    "Grow Home",1  
    "Mad Max",3

## Combine

This mode runs the output of the `extract` mode against a downloaded CSVexport from http://steam.tools/cards/ (great work, guys!)
  
    steamtradingcardoverview combine export.csv STC_set_data.csv
  
It generates a list of your games with available cards and current (average) sale worth. Sample:

    "name","cards remaining","total price"
    "12 Labours of Hercules IV: Mother Nature",1,0.06
    "12 Labours of Hercules V: Kids of Hellas",4,0.20
    "Grind Zones",4,0.20
