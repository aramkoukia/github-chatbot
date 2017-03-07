# DevOrb
Team collaboration and DevOps in one place

<img src="https://github.com/aramkoukia/github-chatbot/blob/master/Documents/Robot-clip-art.png" width="100">

 
#High Level Architecture
## Diagram
![alt text](https://github.com/aramkoukia/github-chatbot/blob/master/Documents/Build%20Release%20Bot.jpg "High Level Architecture Diagram")

## Technologies for each component
###  1. Bot Connector
* We can use microsoft Bot Framework to register the bot and add the various channels (Slack, Skype and MS Teams) to it.

### 2. Bot Service
* Asp.Net Web API service. 
* Hosted in Azure. (AWS?)
* Recieves messages from the various channels

### 3. Text recognition service (AI)
* Asp.Net Web API service. 
* Hosted in Azure. (AWS?)
* Integrates with Linguistics Analytics API.
* Integrates with Text Analytics API.


### 4. Build, Release, PM service (Seperate service for each aspect?)
* Asp.Net Web API service. 
* Hosted in Azure. (AWS?)
* Integrates with Github.
* Integrates with VSTS.
* Integrates with Bitbucket.
* Integrates with Jira.

### 5. DocumentDB (How about SQL Azure instead? Or AWS equivalent)
* Stores information about companies, teams, users.
* Hosted in Azure. (AWS?)
* Stores information about components, build, environments, projects for each team.

### Security
#### 1. Bot Service Security
* Protected with AppId and App Password

#### 2. Text recognition Service Security
* 

#### 3. Build, Release, PM Service Security
*

#### 4. DocumentDB Security
*

By: [Aram Koukia](https://koukia.ca) 
