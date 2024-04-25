using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WFC : MonoBehaviour
{
    // Variables to hold dimensions of the matrix
    public int width = 5;
    public int height = 5;

    // Empty matrix
    private int[,] matrix;

    private int[,] map;

    void Start()
    {
        // Initialize the matrix
        InitializeMatrix();
    }

    public int GetNeighborBits(int[,] matrix, int x, int y, int ind)
    {
        int result = 0;

        // Check if there is a 0 to the North
        if (y==0)
        {
            result |= 1 << 3;
        }
        else if (matrix[x, y - 1] == ind)
            result |= 1 << 3; // Set the bit representing North (bit 3)

        // Check if there is a 0 to the East
        if (x==width-1)
        {
            result |= 1 << 2;
        }
        else if (matrix[x + 1, y] == ind)
            result |= 1 << 2; // Set the bit representing East (bit 2)

        // Check if there is a 0 to the South
        if (y==height-1)
        {
            result |= 1 << 1;
        }
        else if (matrix[x, y + 1] == ind)
            result |= 1 << 1; // Set the bit representing South (bit 1)

        // Check if there is a 0 to the West
        if (x==0)
        {
            result |= 1;
        }
        else if (matrix[x - 1, y] == ind)
            result |= 1; // Set the bit representing West (bit 0)

        return result;
    }

    public int GetCornerBits(int[,] matrix, int x, int y,int ind)
    {
        int result = 0;

        if (matrix[x + 1, y - 1] == ind)
            result |= 1 << 3;

        if (matrix[x - 1, y - 1] == ind)
            result |= 1 << 2;

        if (matrix[x + 1, y + 1] == ind)
            result |= 1 << 1;

        if (matrix[x - 1, y + 1] == ind)
            result |= 1;

        return result;
    }


    public bool CheckWall(int[,] matrix, int x, int y)
    {
        int check = GetNeighborBits(matrix, x,y,0);
        bool wallC=false;

        if(((check & (1 << 3)) != 0)&&((check & (1 << 2)) != 0)&&(matrix[x+1,y-1]==0))
        {
            wallC=true;
        }
        else if(((check & (1 << 2)) != 0)&&((check & (1 << 1)) != 0)&&(matrix[x+1,y+1]==0))
        {
            wallC=true;
        }
        else if(((check & (1 << 1)) != 0)&&((check & (1 << 0)) != 0)&&(matrix[x-1,y+1]==0))
        {
            wallC=true;
        }
        else if(((check & (1 << 0)) != 0)&&((check & (1 << 3)) != 0)&&(matrix[x-1,y-1]==0))
        {
            wallC=true;
        }

        return wallC;
    }

    void GenerateShape(int[,] matrix, int x, int y, ref float chance_w, int width)
    {
        if(matrix[x,y]!=-1 || x>=width/2+(width%2))
        {
            return;
        }

        matrix[x, y] = 9;
        chance_w-=0.3f;

        // Create a list of directions
        List<(int, int)> directions = new List<(int, int)>
        {
            (1, 0),  // Right
            (-1, 0), // Left
            (0, 1),  // Down
            (0, -1)  // Up
        };

        // Shuffle the list of directions
        Shuffle(directions);

        // Recursively generate the shape in random directions
        foreach (var (dx, dy) in directions)
        {
            // Debug.Log(x+" "+y);
            // Debug.Log(dx+" "+dy);
            if (x+dx>=width/2+(width%2) || (GetCornerBits(matrix,x+dx,y+dy,1) & 0b1111)!=0 /* || System.Math.Abs(y + dy)==System.Math.Abs(x + dx)*/)
            {
                // Debug.Log("rekt");
                continue;
            }
            else
            {
                 if(Random.value>=chance_w)
                {
                    // Debug.Log("rekt2");
                    continue;
                }
                else
                {
                    GenerateShape(matrix, x + dx, y + dy, ref chance_w, width);
                }
            }
        }
    }

    // Function to shuffle a list
    void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    void repaint(int[,] matrix, int x, int y, int a, int b)
    {
        for (int i=2;i<y;i++)
        {
            for (int j = 2; j < x; j++)
            {
                if (matrix[i,j]==a)
                {
                    matrix[i,j]=b;
                }
            }
        }
    }

    void MatrixToMap()
    {
        int neighborBits;
        for (int i=0; i < width;i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (matrix[i,j]==1)
                {
                    neighborBits = GetNeighborBits(matrix,i,j,0);
                    switch (neighborBits)
                    {
                        case 0b0000:
                            map[i,j]=0;
                            break;
                        case 0b0001:
                            map[i,j]=1;
                            break;
                        case 0b0010:
                            map[i,j]=2;
                            break;
                        case 0b0011:
                            map[i,j]=3;
                            break;
                        case 0b0100:
                            map[i,j]=4;
                            break;
                        case 0b0101:
                            map[i,j]=5;
                            break;
                        case 0b0110:
                            map[i,j]=6;
                            break;
                        case 0b0111:
                            map[i,j]=7;
                            break;
                        case 0b1000:
                            map[i,j]=8;
                            break;
                        case 0b1001:
                            map[i,j]=9;
                            break;
                        case 0b1010:
                            map[i,j]=10;
                            break;
                        case 0b1011:
                            map[i,j]=11;
                            break;
                        case 0b1100:
                            map[i,j]=12;
                            break;
                        case 0b1101:
                            map[i,j]=13;
                            break;
                        case 0b1110:
                            map[i,j]=14;
                            break;
                        case 0b1111:
                            map[i,j]=15;
                            break;
                        default:
                            map[i,j]=16;
                            break;
                    }
                }
                else
                {
                    map[i,j]=0;
                }
            }
        }
    }
    

    public void InitializeMatrix()
    {
        // Create a new empty matrix with dimensions width x height
        matrix = new int[width, height];

        // Populate the matrix with default values (e.g., -1)
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                matrix[i, j] = -1; // Set all elements to -1
            }
        }

        // Set border elements to 1
        for (int i = 0; i < width/2+(width%2); i++)
        {
            matrix[i, 0] = 1;           // Top border
            matrix[i, height - 1] = 1;  // Bottom border
        }
        for (int j = 0; j < height; j++)
        {
            // matrix[0, j] = 1;           // Left border
            // matrix[width - 1, j] = 1;   // Right border
        }

        int turned = 0;
        int last_t=-10;
        float chance_t=0.05f;
        float chance_x;
        float chance_y;
        float chance_p=0.03f;
        float chance_w;

        //generate border and border events
        for (int j = 1; j < height-1; j++)
        {
            matrix[0, j] = 1;
            //border hill
            if (((Random.value < chance_t && j>=4 && j<height-4) || (Random.value < 2*chance_t && j==1) || (Random.value < 3*chance_t && matrix[0, j-1] == 0)) && j-last_t>3)
            {
                chance_t-=0.02f;
                turned+=1;
                chance_x= 1;
                chance_y= 1;

                int pos_x=0;
                int pos_y=0;

                //border hill on x axis
                do
                {
                    pos_x+=1;
                    matrix[pos_x, j] = 1;
                    if (pos_x>=3)
                    {
                        chance_x-=0.3f;
                    }
                }
                while(Random.value < chance_x);

                //border hill on y axis
                while(Random.value < chance_y && j+pos_y<height-5)
                {
                    pos_y+=1;
                    for (int  k = 0; k <= pos_x; k++)
                    {
                        matrix[k, j+pos_y] = 1;
                    }
                    if (pos_y>=4)
                    {
                        chance_y-=0.3f;
                    }
                }
                j+=pos_y;
                last_t=j;
            }

            //portals
            else if ((Random.value < chance_p || (Random.value < 3*chance_p && (matrix[1, j-1] == 1 || matrix[1, j+1] == 1))) && matrix[0, j-1] !=0 && j!=2 && j!=height-3)
            {
                matrix[0, j]=0;
                matrix[1, j]=0;
                chance_p-=0.02f;
            }
        }

        //phantom cage
        for (int i=width/2+(width%2)-3; i<width/2+(width%2); i++)
        {
            for (int j=height/2+(height%2)-2; j<=height/2+(height%2); j++)
            {
                if (i==width/2+(width%2)-3 || j==height/2+(height%2)-2 || j==height/2+(height%2))
                {
                    matrix[i, j] = 1;
                }
            }

        }
        
        //first empty layer
        int close1;
        int close2;
        int corner;
        for (int j = 1; j < height-1; j++)
        {
            for (int i=1;i < width/2+(width%2); i++)
            {
                if (matrix[i, j]==-1)
                {
                    close1=GetNeighborBits(matrix,i,j,1);
                    close2=GetCornerBits(matrix,i,j,1);
                    if((close1 & 0b1111)!=0)
                    {
                        matrix[i, j] = 0;
                    }
                    else if((close2 & 0b1111)!=0)
                    {
                        matrix[i, j] = 0;
                    }
                }
            }
        }

        //generating the core
        for (int j = 1; j < height-1; j++)
        {
            for (int i=1;i < width/2+(width%2); i++)
            {
                
                if (matrix[i, j]==-1)
                {
                    close1=GetNeighborBits(matrix,i,j,1);
                    close2=GetCornerBits(matrix,i,j,1);
                    if((close1 & 0b1111)!=0)
                    {
                        matrix[i, j] = 0;
                    }
                    else if((close2 & 0b1111)!=0)
                    {
                        matrix[i, j] = 0;
                    }
                    else if(CheckWall(matrix,i,j))
                    {
                        chance_w=1.4f;
                        GenerateShape(matrix,i,j,ref chance_w, width);
                        repaint(matrix,height-1,width/2+(width%2),9,1);
                    }
                }
            }
        }

        //reset map
        // for (int j = 1; j < height-1; j++)
        // {
        //     for (int i=1;i < width/2+(width%2); i++)
        //     {
        //         if(matrix[i,j]==0 && matrix[i,j+1]==0 && matrix[i+1,j+1]==0 && matrix[i+1,j]==0)
        //         {
        //             for (i=0;i<width;i++)
        //             {
        //                 for (j=0;j<height;j++)
        //                 {
        //                     matrix[i,j]=0;
        //                 }
        //             }
        //             InitializeMatrix();
        //             break;
        //         }
        //     }
        // }

        //mirror map
        for (int i = 0; i < width/2; i++)
        {
            for (int j = 0; j < height; j++)
            {
                matrix[width-i-1, j] = matrix[i, j];
            }
        }

        MatrixToMap();
        
        // Print the matrix
        PrintMatrix();
        GetComponent<MatrixToGrid>().GenerateGrid(map);
    }

    // You can add additional methods for manipulating the matrix as needed

    // Example method to print the matrix for debugging
    void PrintMatrix()
    {
        for (int j = 0; j < height; j++)
        {
            string row = "";
            for (int i = 0; i < width; i++)
            {
                if (map[i, j] == 0)
                {
                    row += $"<color=blue>{map[i, j]}</color> ";
                }
                else
                {
                    row += $"<color=red>{map[i, j]}</color> ";
                }
            }
            Debug.LogFormat(row);
        }
    }
}