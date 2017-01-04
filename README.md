# DevOrb
Team collaboration and DevOps in one place

##Brief Description / Elevator pitch
* A lot of software teams (and other teams) use [Slack](https://slack.com/) for their team communication. Microsoft has also released [Microsoft Teams](https://products.office.com/en-us/microsoft-teams/group-chat-software) for team collaborations. And also Skype and Skype for Business is used alot for team collaboration. 
* A Bot for all of these these systems will be real handy that could listen to team conversations and kick of builds, releases, tests, send email communications, project management and other things. 
* Using Microsoft Bot framework and build Slack and Microsoft Team  
* Integrate with This is [Linguistic Analysis API](https://www.microsoft.com/cognitive-services/en-us/linguistic-analysis-api) and [Text Analytics API](https://www.microsoft.com/cognitive-services/en-us/text-analytics-api) for better Bot interactions 
* **NOTE:** The Linguistic and Text Analytics APIs are not free, and they have pay per call payment policy, which should be considered to see how much will it cost to handle certain number of requests.
  * **Linguistic Analysis API** currently does not have a paid service, and is free and the limit is 5000 calls per month. 
  * **Text Analytics API** is free for 5000 calls per month. for 100K calls it is 150$ per month. for 500K calls it is 500$ per month.
  * Price table: http://bit.ly/2j6sYAk
* Integrate with VSTS and Github 
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
* Maybe pay some marketing companies to spread the word with Twitter, Facebook and other social media.

##Bullet list of brainstorms 
* Can we leverage some Microservices architecture for learning purposes and also for better scalability?

##Are there any companies doing something similar 
* Swipes: http://swipesapp.com/slack/ does some of the project management aspects. not sure what is the PM software it integrates with.
* Howdy: https://www.howdy.ai/  does some of the project management aspects.
* Scrumbot: https://www.scrumbot.co/ collect's the team's status report, and the leader can get the report from the bot instead of talking to individuals for their status.

##Ideas for the product name  
* DevOrb

##Skills and Team
* Service Developer

##Key Features
* **Development Aspects**
 * Code Review Reminders?
 * 

* **Build and Release Aspects**
 * Start a build
 * Start a deployment
 
* **QA Aspects**
 * Run tests
 
* **Project Management Aspects**
 * Create workitems/ bugs, tasks etc
 * Collect status report from team members.
 
* **Monitoring and Diagnostics Aspects**
 * Monitor environments for errors and report to the team


#High Level Architecture
# Diagram
![alt text](https://github.com/daveos/DevOrb/blob/master/Documents/Build%20Release%20Bot.jpg "High Level Architecture Diagram")


