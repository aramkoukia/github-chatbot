# DevOrb
Team collaboration and DevOps in one place

<img src="https://github.com/daveos/DevOrb/blob/master/Documents/Robot-clip-art.png" width="100">

##Brief Description / Elevator pitch
* A lot of software teams (and other teams) use [Slack](https://slack.com/), [Microsoft Teams](https://products.office.com/en-us/microsoft-teams/group-chat-software), Skype and Skype for Business for team collaboration and communications. 
* A Bot for all of these systems will be useful. The bot listens to team conversations and start builds, deploy, run tests, send emails, collect daily scrum status report from team members and other things. 
* Integrate with[Linguistic Analysis API](https://www.microsoft.com/cognitive-services/en-us/linguistic-analysis-api) and [Text Analytics API](https://www.microsoft.com/cognitive-services/en-us/text-analytics-api) for better Bot interactions 
* **NOTE:** The Linguistic and Text Analytics APIs are not free, and they have pay per call payment policy, which should be considered to see how much will it cost to handle certain number of requests.
  * **Linguistic Analysis API** currently does not have a paid service, and is free and the limit is 5000 calls per month. 
  * **Text Analytics API** is free for 5000 calls per month. for 100K calls it is 150$ per month. for 500K calls it is 500$ per month.
  * Price table: http://bit.ly/2j6sYAk
* Integration options: Visual Studio Team Services, Github, Jira, Bitbucket
  * Manage build and deployments (trigger, approve, status report)  
  * Test executions 
  * Report component health 
  * Managing work items / tasks/bugs (lookup, add, edit etc) 
  * Build release 
* Integrate with Outlook/Office 365 for email communications 

##What is in it for Orbital
* Orbital will be in there (as a "Bot") in all the Software Development teams conversations around the world, which some of them could be potential customers for Orbital's software and R&D services.
* This could be a marketing opportunity for Orbital to expose its brand to more software development firms.

##What problems are we going to solve 
* DevOps is a very involved task that deals with many different tools, portals, etc, and this can be simplified and encapsulated as a chat bot.
* Project management, scrum and status report is a time consuming issue, which can be collected by a chat bot and be reported to the whole team so everyone is on the same page.
* 

##Target market 
* All Developer teams 
* All DevOps/Build/Releases teams 
* All Software Project Management teams

##How big is the market 
* There are 11 Million professional developers in the world (http://bit.ly/2i71WEG) 
* If we imagine all teams on average have 10 members working on the same project, there are **1 Million professional teams**. 

##How do we market the product 
* We will list the Bot in the connector's bot directory and people using those connectors can add them if they want.
* Aram can engage the MS MVP community and spread the word in that group (around 4000 developers around the world)
* Orbital website can promote the product, though I don't believe the orbital website gets much traffic.
* Do we need to launch a dedicated website for this product or will it be part of Orbital website(like this one: http://acebot.ai/)
* Maybe pay some marketing companies to spread the word with Twitter, Facebook and other social media.
* 

##Bullet list of brainstorms 
* Can we leverage some Microservices architecture for learning purposes and also for better scalability and performance?
* Some of the API calls like build and release, could take long to finish, so we need to think of mechanisms to notify the bot or the person who initiated the request when it is finished.
* How do we setup the bot for build and release? when a bot is added to the conversation, it needs to know and record, the information about the environemts, components, security tokens, etc. User should pass these information to the Bot and we should store them. So next time user asks for a build of component X, we know where the code is (VSTS or Gitgub path) for each company, to start a build.
* For deployments, we need to know the destination server and all the other stuff that are needed for a deployment (certificates? Azure, vs On-prem?). I think it will only work easily when the deployment is done in some cloud environment (Azure or AWS)
* Is it better to limit the integration to Github or VSTS for the fist release.


##Are there any companies doing something similar 
* Swipes: http://swipesapp.com/slack/ does some of the project management aspects. not sure what is the PM software it integrates with.
* Howdy: https://www.howdy.ai/  does some of the project management aspects.
* Scrumbot: https://www.scrumbot.co/ collect's the team's status report, and the leader can get the report from the bot instead of talking to individuals for their status.

##Ideas for the product name  
* DevOrb
* DevOps Bot
* DevOps AI
* DevOps Ninja
* DevOps Robot
* DevOps Hero

##Skills and Team
* Service Developer

##Key Features
* **Company Onboarding**
 * Collect company's information, repository security information (VSTS/Github path) etc
 *
 
* **Project/Component/Environment Onboarding**
 * Collect Component names, Technology (C#, Java, etc if needed for builds), build definitions, source/repository path etc.
 * Collect Projects name (each project can have multiple components). Bot needs to know which project to create the items on.
 * 
 
* **Development Aspects**
 * Code Review Reminders?
 * Submit a Pull Request
 * Let the developer know when there is a PR assigned to them
 * Let the developer know when a PR is complete or there was conflict
 *

* **Build and Release Aspects**
 * Start a build (specific component)
 * Start a deployment (specific component and specific environment)
 * Report the status of Build and/or Deployment
 * 
  
* **QA Aspects**
 * Run tests (is this possible? Bot might not be aware of the technology of the code!)
 
 
* **Project Management Aspects**
 * Create workitems/ bugs, tasks etc (The workitems are different in VSTS vs Github)
 * Collect status report from team members.
 * **Question** Do we integrate with Jira for project management aspects?
 *
 
* **Monitoring and Diagnostics Aspects**
 * Monitor environments for errors and report to the team (is this possible? it will only work if the system has some diagnostics implemented)
 *
 
#High Level Architecture
## Diagram
![alt text](https://github.com/daveos/DevOrb/blob/master/Documents/Build%20Release%20Bot.jpg "High Level Architecture Diagram")

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

