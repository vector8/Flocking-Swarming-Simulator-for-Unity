Flock & Swarm Simulator - Copyright Citadel Technologies

See https://youtu.be/LV8lbEK-YYQ for a video tutorial.

Instructions

1.) Add the Flock prefab, found in the CitadelTechnologies/Prefabs/ folder, to your scene.
2.) On the Flock GameObject, change the "Member Prefab" field to whatever prefab you want to use for each of the flock members (e.g. bird model, fly sprite, etc.).
3.) Optional: Set the "Follow Target" field to any transform in your scene. This will cause the flock to follow that transform when the "Swarm Factor" field is set to a value greater than 0.
4.) Set each of the parameter fields however you like. A description of each is as follows:

	Pull Factor - This determines how much each flock member wants to move towards the average position of the entire flock. 
	
	Separation Factor - This determines how much each flock member will try to distance itself from other nearby flock members. This relies on the Neighbour Distance parameter, which tells the member how far to look when looking for nearby members.
	
	Swarm Factor - This determines how much the flock will be drawn towards the "Follow Target" transform.
	
	Inertia Factor - This determines how much each member will try to align itself with the other members of the flock.
	
	Neighbour Distance - This tells the member how far to look when looking for nearby members for calculating the Separation Factor.
	
	Max Velocity - This simply limits the maximum velocity of each individual flock member, which can keep the members from going crazy if the other parameters are too high.

Example sets of parameters:

School of fish:
	Pull Factor: 0.83
	Separation Factor: 15
	Swarm Factor: 1
	Inertia Factor: 1.72 
	Neighbour Distance: 1
	Max Velocity: 5
	
Swarm of insects:
	Pull Factor: 1
	Separation Factor: 137.9
	Swarm Factor: 28.2
	Inertia Factor: -82.9 
	Neighbour Distance: 1
	Max Velocity: 5
	