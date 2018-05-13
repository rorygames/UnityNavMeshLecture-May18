# Unity NavMesh Lecture May 18

Project for the material from the lecture demonstration.
Remember to reference if anything is used in assignments.

## Requirements
- Unity 2018.1 (the project was created with 2018.1.0f2).
- VS2017 (Unity 2018 has issues with VS versions prior to 2017, community works fine).

## Useful Information
- You must use the **UnityEngine.AI** namespace to access the navmesh functions.
- The new Unity NavMesh code is not currently found within the engine. You need to download it from the [NavMeshComponents GitHub Repo](https://github.com/Unity-Technologies/NavMeshComponents). These components are available from version 5.6 and up, you need to change the branch you are on to access them.
- You only need the "Assets/NavMeshComponents" folder from the above repo.
- A realtime NavMesh scene can be found in the above repo in the scene called "drop_plank".
- Old navmesh creation is bound to the *scene*, it is built by using the bake button in the navigation window and takes into account the paramets set in the bake tab. New navmesh creation is bound to the *object or prefab* allowing you to quickly swap objects in and out without having to load entire scenes. 
- NavMeshAgents should not have Rigidbody components. If you include them you will encounter multiple issues with angled navmesh areas.
- You can use NavMeshAgents just like a rigidbody, applying forces to change their direction. **This means you can use them for both the player and your AI units.**
- NavMeshObstacles don't *have* to carve the navmesh. If they carve the navmesh it will require the navmesh to update, thus updating every unit that is currently on a path. If you set them to not carve then all the agents will utilise their obstacle avoidance parameters instead when near to the object.
- NavMeshObstacles (NMO) are limited in shape when compared to NavMeshModifers (NMM), only allowing for cubes or cylindes. As mentioned above though, NMO's do not require the NavMesh to be rebuilt to work. NMM's require the NavMesh to rebuilt everytime to work at all. Keep this in mind when utilising large NavMeshes.
- You can use NavMeshLinks to create drops, jump spots, or to simply connect smaller navmeshes together to create a larger one.

>Visual representation of NavMeshObstacle carving differences.
![Visual representation of NavMeshObstacle carving differences](https://i.imgur.com/iJn7IqA.png)
