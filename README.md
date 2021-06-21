# everlight-technical-challenge
Everlight Radiology technical challenge

## How to run
Simply type "dotnet run [depth]" to run and test the program. The program will write the required information to console.

## Data structure explain:
The program uses a heap array to represent a tree. The root node start at position 1 of the heap array, and the left child node of each node is 2*i, and the right chid node is 2*i + 1.

After storing all the gate directions, the heap array also store the containers' index, and print it out as ASCII characters.

### Prediction algorithm explain:
The predicted empty container is the container whose ancesters all have gates in opposite direction.

If depth is 1 and a node's gate is facing itself, it only need once to get a ball. Otherwise the node will need two times to get a ball.

Which means if depth is 2, worst case is 4 times, and depth 3 will be 8 times. However, depth 3 has only 7 balls, so all we need is to find the container that has all the gates facing opposite direction.

### Run balls explain:
Each time a ball finds a container, the container index in the heap array will be changed to -99. After all balls done, the only container that's not -99 will be the empty one.

You can compare the last WriteLine with the prediction at beginning.

### Example output with depth 3
![example output in console](https://github.com/tenglongroy/everlight-technical-challenge/raw/main/example-output.png?raw=true)
