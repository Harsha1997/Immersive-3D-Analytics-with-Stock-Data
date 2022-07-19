# Immersive-3D-Analytics-with-Stock-Data
We present a novel way of visualizing stock data in an immersive 3D virtual environment to enable a superior understanding and analysis of financial data. Our design consists of 4 virtual screens in a workplace environment to stimulate the feeling of working at an esteemed organization. Each screen will be capable of displaying stock data of 2 companies, totaling 8 different organizational stock data which can be compared simultaneously. We also offer numerous time horizons for each screen to choose from for the user and provide accessibility for improved user experience.

Please look into the paper added in the github repository for more information.

## Introduction
Currently traders use multiple desktop screens to view different stock data over different time periods. While this has been useful, it is a tedious process and requires monetary investment for the hardware, if the user wants to work comfortably on full scale. The graphs are 2-dimensional (2D) in most cases, and users might miss out on some important trends in the data. Even if the data is represented in 3-dimensional (3D) graphs, the user can not fully perceive the 3D graph on the 2D screens. So we want to enable the user with a more engaging and superior experience by using Virtual Reality. We want to provide the user a better functionality where they can interact and understand stock data better and help them take faster decisions. This can result in
profits and make it a more interesting for the user. We plan to build this using Unity and then eventually load into Oculus quest for the client to work with.

## Pre Requisites:
Download the following items from this [google drive](https://drive.google.com/drive/folders/1xQmMLIel9XJZX3yB4-2y0wIobRUTY09A?usp=sharing): 
1) Place the Library under the main section
2) Place the Oculus folder and meta-data (not required) under the Assets folder
3) Preferred Oculus Quest 2 (Oculus Quest will also work)
4) A decently powered CPU and GPU capable of running Unity Hub smoothly

## Dataset

We have scraped the data from Yahoo Finance. We have chosen top 10 companies, which have high impact on the American economy and ignite interest for the laymen user. The companies along with their stock tickers: 
• Apple : APPL
• Microsoft : MSFT
• Berkshire Hathaway : BRK-B
• Meta Platforms, Inc. : FB
• Nvidia : NVDA
• Walmart : WMT
• JP Morgan : JPM
• Goldman Sachs : GS
• American Express : AXP
• UnitedHealth Group : UNH

## How to use

We have the follwoing capabilities : 
1) Stock Dropdown: Each user can select upto two different companies per screen. The user can change the company selection any time they want and the graph changes according to the selection they have made.
2) Time Horizon: We have chosen 4 different time horizons ranging from weeks to years. By default we had the Week selected which showed the data for the past one week.

The main capability is driven by the script which is run in Unity Hub.
