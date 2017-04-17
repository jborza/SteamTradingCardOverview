# SteamTradingCardOverview

Hi Steam trading card collectors! This is my tool to generate an overview of how many Steam trading cards are there available in your account and how much are they currently worth.

Two usage modes:  

## Extract

- first you need to read pasted contents from "badges" page - e.g. http://steamcommunity.com/id/USERNAME/badges/ from the standard input and generates a comma separated list of games and number of remaining steam trading cards. 

This generates a CSV files Sample output:

    "Grow Home",1  
    "Mad Max",3

Usage is easier if you paste your data into a .txt file, and then call the tool as
 
    steamtradingcardoverview extract <pasted.txt >export.csv

## Combine

Then you run the tool against a downloaded CSVexport from http://steam.tools/cards/ (great work, guys!)
  
    `steamtradingcardoverview combine export.csv STC_set_data.csv`
  
It generates a list of your games with available cards and current (average) worth.