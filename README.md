# Machine Learning 
Once sparked with interest in ML agents, I needed a way to learn about them in a familiar way. For me this came from the open source project, [_ML-Agents for Unity_](https://github.com/Unity-Technologies/ml-agents). 

## Goal
The goal of this project is relatively simple. I wanted to learn more about ML agents and see how it might be possible to create in-depth AI characters without implementing and learning a new design pattern. 

## Agents

### Prey
The first ML Agent I trained was rewarded for coming in contact with a "home" on a platform. If the agent fell off the platform, it was given a punishment. To ensure that I was training the agent properly, I gave the agent two observables, their current position
and the position of their home. Eventually the agent was proficient in finding their home within moments of starting. However, soon I introduced a third observable to the agent, a predator's location. Just like the edge of the platform, the prey would be punished if 
they made contact with the predator. Now, after some training, I had a prey that would have the goal of avoiding the edge of the platform and the predator as they found their way home. 

### Predator 
The predator brain is actually a copy of the prey brain from before the prey learned about the predator. To the predator, "home" was the prey which is now a location that can move. Long with this, if the prey managed to get away from them, into their home, the predator
was punished. As you might have seen in the image on my portfolio website, the predator learned that it must find a way to get the prey before it could be outmaneuvered by the prey's quicker movement speed. 

## Conclusion
At the end of the project, I learned that training these brains against one another always concluded in the brain with the most recent strategy to be the overwhelming winner. However, it was important to note that picking up and experimenting with ML Agents 
allowed me to better understand [Reinforcement Learning](https://en.wikipedia.org/wiki/Reinforcement_learning) at a fundamental level without needing to get into Machine Learning Algorithms. 
