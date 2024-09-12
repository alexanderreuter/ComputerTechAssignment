 # Computer technology assignment
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
## **Author:**
Alexander Reuter

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
## **Unity version: 2022.3.40f1**

Templete: 
Unity’s Universal Render Pipeline pre-configured with 2D Renderer.

Additional packages used: 
* Input System
* Entities
* Entities Graphics

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
## **System in performant conscious fashion:**

### **Code structure:**

For this project I've had my primary focus on keeping the code optimized. I've been conscious about using as few reference types as possible and tried to structure my code as efficient as possible. For example, I've taken full advantage of the structual benefites of ECS, using the principle of composition over inheritance, meaning entities are defined by data/components and not by hierarchy. This also heavily reduce object dependency and naturally create very efficient systems. Another example would be how I structure my own code. If there's a requirement that has to be met to run a specific system I'll have that check at the top, and if it's not met, break out of the scope. This is to make sure I don't run any unnecessary code.

Example:

    public void OnUpdate(ref SystemState state)
    {
        if (!SystemAPI.TryGetSingleton<InputComponent>(out var input) || !input.isShooting)
            return; 
        
        // Rest of the code...
    }


### **Jobs:**
For almost all of my systems I'm using Unity jobs to exceute the systems behavior. The reason for this is to make sure that there's room to run the logic and to be able to utilize all available CPU cores. 

The only system where I'm not using jobs is for input. This is because input needs immediate reaction, so I don't wanna add unnecssesary latency. Reading the input is also a low-cost operation and is only used for one entity, the player. Therfore there's no need for paralleism which is one of the main advantages with jobs.

For all the systems that're used with enteties that there're a lot of, in this case bullets and enemies, I'm using parallel jobs. Since there's a high likelyhood that these systems are gonna be handling a lot of data we want to be able to use as much compute power as possible from the CPU. 

With jobs I'm also using [BurstCompilie] to further enhance optimizing performance-heavy operations. Especially for parallel jobs since it combines multithreading with low-level code optimizations.

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
## **Reflections:**
One of the take aways from this assignment is that working with ECS/data oriented approach requires a very different mindset compared to working with traditional object oriented principles. ECS is feels like it requires more micro management since it seperates the data from the behavior, but once you get the hang of it, it's not that complicated. Also, once you understand the underlying advantages of ECS, such as how it's optimized for cache and you're able to take advantage of contiguous data in the memory, which is I assume, the main reason we'd like to aviod reference types when using DOTS.  

Another things I'd like to point out is that I could've added a lot more features to the game. Such as collision, score, etc. However, I decided to focus more on the optimization and understand underlying structure and princples of ECS/DOTS instead, since that's not somthing I've been working with before. 

