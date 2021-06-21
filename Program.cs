using System;
using System.Collections.Generic;

namespace everlight_technical_challenge
{
    class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length > 0){
                int depth = Convert.ToInt32(args[0]);
                BallTree test = new BallTree(depth);
            }
        }
    }
    class BallTree
    {
        List<int> heap;
        Random random;
        public BallTree(int depth)
        {

            this.heap = new List<int>();
            this.heap.Add(0);
            this.random = new Random((int)DateTime.Now.Ticks);
            this.generateTree(this.heap, depth);
            int predictContainer = this.predictNoBall(this.heap, depth);
            this.printTreeStructure(this.heap, depth);
            Console.WriteLine("The predicted container is [" + (char)('A' + predictContainer) + "]\n");
            Console.WriteLine("----------------");
            this.traverseTree(this.heap, depth);
            this.checkEmptyContainer(this.heap, depth);
        }

        void generateTree(List<int> heapList, int depth){
            int totalLength = (int)Math.Pow(2, depth);
            // store gateSide into heapList
            for(int ind = 1; ind < totalLength; ind ++){
                int gateSide = this.random.Next(-1, 1) < 0 ? -1 : 1;    // if -1 means the gate is opening left, 1 means opening right
                heapList.Add(gateSide);
            }
            // the tree leaves store container name into as 1, 2, 3, 4.... reflecting A, B, C, D....
            for(int ind = 1; ind <= totalLength; ind ++){
                heapList.Add(ind);
            }
        }

        void printTreeStructure(List<int> heapList, int depth){
            Console.WriteLine("The heap array for gate side is:");
            for(int ind = 1; ind < heapList.Count; ind++){
                /* Console.Write(heapList[ind]); */
                if (ind < Math.Pow(2, depth)){  // gate side
                    Console.Write(heapList[ind]);
                }
                else{   // container name
                    Console.Write((char)(heapList[ind]-1 + 'A'));
                }
                
                Console.Write(" ");
            }
            Console.WriteLine("");
        }

        int predictNoBall(List<int> heapList, int depth){
            int nextInd = 1, currentGateSide = 0;
            while(Math.Pow(2, depth) > nextInd){    // keep looping as long as nextInd not over gate index
                currentGateSide = heapList[nextInd];
                nextInd = nextInd * 2 + (currentGateSide < 0 ? 1 : 0);    // if gateSide opening to left, go find right sub-tree; vice versa
            }
            int containerInd = nextInd - (int)Math.Pow(2, depth);
            return containerInd;
        }

        void traverseTree(List<int> heapList, int depth){
            int totalBall = (int)Math.Pow(2, depth) - 1;
            for (int ind = 0; ind < totalBall; ind ++){
                this._traverseTree(heapList, depth, ind);
            }
        }
        void _traverseTree(List<int> heapList, int depth, int ballInd){
            List<int> path = new List<int>();
            int nextInd = 1, currentGateSide = 0;
            while(Math.Pow(2, depth) > nextInd){
                path.Add(nextInd);
                currentGateSide = heapList[nextInd];
                heapList[nextInd] *= -1;
                nextInd = nextInd * 2 + (currentGateSide < 0 ? 0 : 1);
            }
            heapList[nextInd] = -99;
            int containerInd = nextInd - (int)Math.Pow(2, depth);
            char containerName = (char)('A' + containerInd);

            Console.Write("Path for No." + ballInd + " ball -> ");
            Console.WriteLine(string.Join(" ", path));

            Console.WriteLine("Reach container : " + containerName);

            Console.Write("Current Gate direction ==>> ");
            Console.WriteLine(string.Join(" ", heapList.GetRange(1, heapList.Count-1)));
            
            Console.WriteLine("----------------");
        }

        void checkEmptyContainer(List<int> heapList, int depth){
            int containerStartInd = (int)Math.Pow(2, depth);
            for(; containerStartInd < heapList.Count; containerStartInd ++){
                if (heapList[containerStartInd] != -99){
                    Console.WriteLine("\nEmpty container is [" + (char)('A' + containerStartInd - (int)Math.Pow(2, depth)) + "]");
                }
            }
        }
    }
}
