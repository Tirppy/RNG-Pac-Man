using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class WFCF : MonoBehaviour
{
    // Variables to hold dimensions of the matrix
    public int width = 5;
    public int height = 5;

    // Empty matrix
    private int[,] matrix;
    private HashSet<int>[,] possibleStates;
    private int[,] map;

    void Start()
    {
        // Initialize the matrix
        InitializeMatrix();
    }

    public void MatrixToMap()
    {
        int neighborBits;
        int cornerBits;
        for (int i=0; i < width;i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (matrix[i,j]==1)
                {
                    neighborBits = GetNeighborBits(matrix,i,j);
                    switch (neighborBits)
                    {
                        case 0b0011:
                            map[i,j]=7;
                            break;
                        case 0b0110:
                            map[i,j]=8;
                            break;
                        case 0b0111:
                            map[i,j]=4;
                            break;
                        case 0b1001:
                            map[i,j]=6;
                            break;
                        case 0b1011:
                            map[i,j]=3;
                            break;
                        case 0b1100:
                            map[i,j]=5;
                            break;
                        case 0b1101:
                            map[i,j]=2;
                            break;
                        case 0b1110:
                            map[i,j]=1;
                            break;
                        case 0b1111:
                            cornerBits = GetCornerBits(matrix,i,j);
                            if(i==0 && j==0)
                                Debug.Log(i+" "+j+" | "+neighborBits+" "+cornerBits);
                            switch (cornerBits)
                            {
                                case 0b0111:
                                    map[i,j]=5;
                                    break;
                                case 0b1011:
                                    map[i,j]=6;
                                    break;
                                case 0b1101:
                                    map[i,j]=8;
                                    break;
                                case 0b1110:
                                    map[i,j]=7;
                                    break;
                                default:
                                    map[i,j]=0;
                                    break;
                            }
                            break;
                    }
                }
                else
                {
                    map[i,j]=0;
                }
            }
        }
        for (int i=width/2+(width%2)-3; i<=width/2+(width%2)+1; i++)
        {
            for (int j=height/2+(height%2)-2; j<=height/2+(height%2); j++)
            {
                if (i==width/2+(width%2)-3 && j==height/2+(height%2)-2)
                    map[i,j]=9;
                else if (i==width/2+(width%2)-3 && j==height/2+(height%2))
                    map[i,j]=11;
                else if (i==width/2+(width%2)+1 && j==height/2+(height%2)-2)
                    map[i,j]=10;
                else if (i==width/2+(width%2)+1 && j==height/2+(height%2))
                    map[i,j]=12;
                else if (i>width/2+(width%2)-3 && i<width/2+(width%2)+1 && j==height/2+(height%2)-2)
                    map[i,j]=13;
                else if (i==width/2+(width%2)+1 && j>height/2+(height%2)-2 && j<height/2+(height%2))
                    map[i,j]=14;
                else if (i>width/2+(width%2)-3 && i<width/2+(width%2)+1 && j==height/2+(height%2))
                    map[i,j]=15;
                else if (i==width/2+(width%2)-3 && j>height/2+(height%2)-2 && j<height/2+(height%2))
                    map[i,j]=16;
                else
                    map[i,j]=0;
            }
        }
    }

    public bool IsAreaEmpty(int startX, int startY, int endX, int endY)
    {
        for (int i=startX;i<=endX;i++)
        {
            for (int j = startY; j <= endY; j++)
            {
                if (matrix[i,j]!=-1)
                {
                    return(false);
                }
            }
        }
        return(true);
    }

    public bool CanFitShape(int shape, int x, int y)
    {
        switch (shape)
        {
            case 11:
                if (y+1<height && IsAreaEmpty(x,y,x+3,y+1))
                    return(true);
                else
                    return(false);
                break;
            case 12:
                if (y+3<height && IsAreaEmpty(x,y,x+1,y+3))
                    return(true);
                else
                    return(false);
                break;
            case 21:
                if (y+2<height && IsAreaEmpty(x,y,x+3,y+2))
                    return(true);
                else
                    return(false);
                break;
            case 22:
                if (y+3<height && IsAreaEmpty(x,y,x+2,y+3))
                    return(true);
                else
                    return(false);
                break;
            case 31:
                if (y+2<height && IsAreaEmpty(x,y,x+4,y+2))
                    return(true);
                else
                    return(false);
                break;
            case 32:
                if (y+4<height && IsAreaEmpty(x,y,x+2,y+4))
                    return(true);
                else
                    return(false);
                break;
            case 41:
                if (y+1<height && IsAreaEmpty(x,y,x+4,y+1))
                    return(true);
                else
                    return(false);
                break;
            case 42:
                if (y+4<height && IsAreaEmpty(x,y,x+1,y+4))
                    return(true);
                else
                    return(false);
                break;
            case 51:
                if (y+4<height && IsAreaEmpty(x,y,x+5,y+1) && IsAreaEmpty(x+2,y,x+3,y+4))
                    return(true);
                else
                    return(false);
                break;
            case 52:
                if (y+5<height && IsAreaEmpty(x,y,x+1,y+5) && IsAreaEmpty(x,y+2,x+4,y+3))
                    return(true);
                else
                    return(false);
                break;
            case 53:
                if (y+4<height && x-2>=0 &&  IsAreaEmpty(x,y,x+1,y+4) && IsAreaEmpty(x-2,y+3,x+3,y+4 ))
                    return(true);
                else
                    return(false);
                break;
            case 54:
                if (y+5<height && IsAreaEmpty(x,y,x+1,y+5) && IsAreaEmpty(x,y+2,x+4,y+3))
                    return(true);
                else
                    return(false);
                break;
            case 61:
                if (y+4<height && IsAreaEmpty(x,y,x+7,y+1) && IsAreaEmpty(x+3,y,x+4,y+4))
                    return(true);
                else
                    return(false);
                break;
            case 62:
                if (y+7<height && IsAreaEmpty(x,y,x+1,y+7) && IsAreaEmpty(x,y+3,x+4,y+4))
                    return(true);
                else
                    return(false);
                break;
            case 63:
                if (y+4<height && x-3>=0 && IsAreaEmpty(x,y,x+1,y+3) && IsAreaEmpty(x-3,y+3,x+4,y+4))
                    return(true);
                else
                    return(false);
                break;
            case 64:
                if (y+7<height && IsAreaEmpty(x,y,x+1,y+7) && IsAreaEmpty(x,y+3,x+4,y+4))
                    return(true);
                else
                    return(false);
                break;
            case 71:
                if (y+3<height && IsAreaEmpty(x,y,x+8,y+1) && IsAreaEmpty(x+4,y,x+5,y+3))
                    return(true);
                else
                    return(false);
                break;
            case 72:
                if (y+8<height && IsAreaEmpty(x,y,x+1,y+8) && IsAreaEmpty(x,y+3,x+3,y+4))
                    return(true);
                else
                    return(false);
                break;
            case 73:
                if (y+3<height && x-3>=0 && IsAreaEmpty(x,y,x+1,y+3) && IsAreaEmpty(x-3,y,x+5,y+3))
                    return(true);
                else
                    return(false);
                break;
            case 74:
                if (y+8<height && x-2>=0 && IsAreaEmpty(x,y,x+1,y+8) && IsAreaEmpty(x-2,y+4,x+1,y+5))
                    return(true);
                else
                    return(false);
                break;
            case 81:
                if (y+4<height && IsAreaEmpty(x,y,x+4,y+1) && IsAreaEmpty(x,y,x+1,y+4))
                    return(true);
                else
                    return(false);
                break;
            case 82:
                if (y+4<height && IsAreaEmpty(x,y,x+1,y+4) && IsAreaEmpty(x,y+3,x+4,y+4))
                    return(true);
                else
                    return(false);
                break;
            case 83:
                if (y+4<height && x-3>=0 && IsAreaEmpty(x,y,x+1,y+4) && IsAreaEmpty(x-3,y+3,x+1,y+4))
                    return(true);
                else
                    return(false);
                break;
            case 84:
                if (y+4<height && IsAreaEmpty(x,y,x+4,y+1) && IsAreaEmpty(x+3,y,x+4,y+4))
                    return(true);
                else
                    return(false);
                break;
            case 91:
                if (y+5<height && IsAreaEmpty(x,y,x+3,y+1) && IsAreaEmpty(x,y,x+1,y+5))
                    return(true);
                else
                    return(false);
                break;
            case 92:
                if (y+3<height && IsAreaEmpty(x,y,x+1,y+3) && IsAreaEmpty(x,y+2,x+5,y+3))
                    return(true);
                else
                    return(false);
                break;
            case 93:
                if (y+5<height && x-2>=0 && IsAreaEmpty(x,y,x+1,y+5) && IsAreaEmpty(x-2,y+4,x+1,y+5))
                    return(true);
                else
                    return(false);
                break;
            case 94:
                if (y+3<height && IsAreaEmpty(x,y,x+5,y+1) && IsAreaEmpty(x+4,y,x+5,y+3))
                    return(true);
                else
                    return(false);
                break;
            case 100:
                if (y+5<height && x-2>=0 && IsAreaEmpty(x,y,x+1,y+5) && IsAreaEmpty(x-2,y,x+3,y+5))
                    return(true);
                else
                    return(false);
                break;
            case 101:
                if (y+2<height && IsAreaEmpty(x,y,x+2,y+2))
                    return(true);
                else
                    return(false);
                break;
            case 102:
                if (y+2<height && IsAreaEmpty(x,y,x+3,y+3))
                    return(true);
                else
                    return(false);
                break;
            default:
                return(true);
                break;
        }
    }

    public void fillArea(int startX, int startY, int endX, int endY)
    {
        for (int i=startX;i<=endX;i++)
        {
            for (int j = startY; j <= endY; j++)
            {
                matrix[i,j]=1;
            }
        }
    }

    public void fillShape(int shape, int x, int y)
    {
        switch (shape)
        {
            case 11:
                fillArea(x,y,x+3,y+1);
                break;
            case 12:
                fillArea(x,y,x+1,y+3);
                break;
            case 21:
                fillArea(x,y,x+3,y+2);
                break;
            case 22:
                fillArea(x,y,x+2,y+3);
                break;
            case 31:
                fillArea(x,y,x+4,y+2);
                break;
            case 32:
                fillArea(x,y,x+2,y+4);
                break;
            case 41:
                fillArea(x,y,x+4,y+1);
                break;
            case 42:
                fillArea(x,y,x+1,y+4);
                break;
            case 51:
                fillArea(x,y,x+5,y+1);
                fillArea(x+2,y,x+3,y+4);
                break;
            case 52:
                fillArea(x,y,x+1,y+5);
                fillArea(x,y+2,x+4,y+3);
                break;
            case 53:
                fillArea(x,y,x+1,y+4);
                fillArea(x-2,y+3,x+3,y+4);
                break;
            case 54:
                fillArea(x,y,x+1,y+5);
                fillArea(x,y+2,x+4,y+3);
                break;
            case 61:
                fillArea(x,y,x+7,y+1);
                fillArea(x+3,y,x+4,y+4);
                break;
            case 62:
                fillArea(x,y,x+1,y+7);
                fillArea(x,y+3,x+4,y+4);
                break;
            case 63:
                fillArea(x,y,x+1,y+3);
                fillArea(x-3,y+4,x+4,y+3);
                break;
            case 64:
                fillArea(x,y,x+1,y+7);
                fillArea(x,y+3,x+4,y+4);
                break;
            case 71:
                fillArea(x,y,x+8,y+1);
                fillArea(x+4,y,x+5,y+3);
                break;
            case 72:
                fillArea(x,y,x+1,y+8);
                fillArea(x,y+3,x+3,y+4);
                break;
            case 73:
                fillArea(x,y,x+1,y+3);
                fillArea(x-3,y,x+5,y+3);
                break;
            case 74:
                fillArea(x,y,x+1,y+8);
                fillArea(x-2,y+4,x+1,y+5);
                break;
            case 81:
                fillArea(x,y,x+4,y+1);
                fillArea(x,y,x+1,y+4);
                break;
            case 82:
                fillArea(x,y,x+1,y+4);
                fillArea(x,y+3,x+4,y+4);
                break;
            case 83:
                fillArea(x,y,x+1,y+4);
                fillArea(x-3,y+3,x+1,y+4);
                break;
            case 84:
                fillArea(x,y,x+4,y+1);
                fillArea(x+3,y,x+4,y+4);
                break;
            case 91:
                fillArea(x,y,x+3,y+1);
                fillArea(x,y,x+1,y+5);
                break;
            case 92:
                fillArea(x,y,x+1,y+3);
                fillArea(x,y+2,x+5,y+3);
                break;
            case 93:
                fillArea(x,y,x+1,y+5);
                fillArea(x-2,y+4,x+1,y+5);
                break;
            case 94:
                fillArea(x,y,x+5,y+1);
                fillArea(x+4,y,x+5,y+3);
                break;
            case 100:
                fillArea(x,y,x+1,y+5);
                fillArea(x-2,y,x+3,y+5);
                break;
            case 101:
                fillArea(x,y,x+2,y+2);
                break;
            case 102:
                fillArea(x,y,x+3,y+3);
                break;
        }
    }

    public void resetMatrix()
    {
        for (int i = 0; i < width/2+(width%2); i++)
        {
            for (int j = 0; j < height; j++)
            {
                matrix[i,j]=-1;
            }
        }
    }

    public void ResetStates()
    {
        for (int i = 0; i < width/2+(width%2); i++)
        {
            for (int j = 0; j < height; j++)
            {
                possibleStates[i,j].Clear();
            }
        }
    }

    public void checkState()
    {
        List<int> states = new List<int> { 11,12,21,22,31,32,41,42,51,52,53,54,61,62,63,64,71,72,73,74,81,82,83,84,91,92,93,94,100,101,102 };
        for (int i = 0; i < width/2+(width%2); i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (matrix[i,j]==-1)
                {
                    foreach (int state in states)
                    {
                        if(CanFitShape(state,i,j))
                        {
                            possibleStates[i, j].Add(state);
                        }
                    }
                }
            }
        }
    }

    public bool HasAnyState(int x, int y)
    {
        return possibleStates[x, y].Count > 0;
    }

    public void chooseState(int x, int y)
    {
        HashSet<int> states = possibleStates[x, y];
        
        int[] statesArray = new int[states.Count];
        states.CopyTo(statesArray);

        int randomIndex = UnityEngine.Random.Range(0, states.Count);

        int randomState = statesArray[randomIndex];
        fillShape(statesArray[randomIndex],x,y);
    }

    public (int, int) findEmpty()
    {
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width/2+(width%2); i++)
            {
                if (matrix[i,j]==-1)
                {
                    if (HasAnyState(i,j))
                    {
                        return (i, j);
                    }
                    else
                    {
                        return (0,-1);
                    }
                }
            }
        }
        return (-1,0);
    }

    public int GetNeighborBits(int[,] matrix, int x, int y)
    {
        int result = 0;

        // Check if there is a 0 to the North
        if (y==0)
        {
            result |= 1 << 3;
        }
        else if (matrix[x, y - 1] == 1)
            result |= 1 << 3; // Set the bit representing North (bit 3)

        // Check if there is a 0 to the East
        if (x==width-1)
        {
            result |= 1 << 2;
        }
        else if (matrix[x + 1, y] == 1)
            result |= 1 << 2; // Set the bit representing East (bit 2)

        // Check if there is a 0 to the South
        if (y==height-1)
        {
            result |= 1 << 1;
        }
        else if (matrix[x, y + 1] == 1)
            result |= 1 << 1; // Set the bit representing South (bit 1)

        // Check if there is a 0 to the West
        if (x==0)
        {
            result |= 1;
        }
        else if (matrix[x - 1, y] == 1)
            result |= 1; // Set the bit representing West (bit 0)

        return result;
    }

    public int GetCornerBits(int[,] matrix, int x, int y)
    {
        int result = 0;

        if ((x==width-1)||(y==0)||(matrix[x + 1, y - 1] == 1))
            result |= 1 << 3;

        if ((x==0)||(y==0)||(matrix[x - 1, y - 1] == 1))
            result |= 1 << 2;

        if ((x==width-1)||(y==height-1)||matrix[x + 1, y + 1] == 1)
            result |= 1 << 1;

        if ((x==0)||(y==height-1)||matrix[x - 1, y + 1] == 1)
            result |= 1;

        return result;
    }

    public void paintRoad()
    {
        int close1;
        int close2;
        for (int i=1;i < width/2+(width%2); i++)
        {
            for (int j = 1; j < height-1; j++)
            {
                if (matrix[i, j]==-1)
                {
                    close1=GetNeighborBits(matrix,i,j);
                    close2=GetCornerBits(matrix,i,j);
                    if(((close1 & 0b1111)!=0) || ((close2 & 0b1111)!=0))
                    {
                        matrix[i, j] = 0;
                    }
                }
            }
        }
    }

    public void generateBorder()
    {
        int lastHillX1=-1;
        int lastHillX2=-1;
        int lastHillY=-1;
        int offset;

        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width/2+(width%2); i++)
            {
                if (i==0)
                {
                    if(j==0 || j==2 || j==3 || j==4 || j==height)
                    {
                        matrix[i,j]=1;
                    }
                    else
                    {
                        if (j-lastHillY>=5 && Random.value<0.05)
                        {
                            offset=UnityEngine.Random.Range(1, 7);
                            if(j+offset!=height-3 && j+offset!=height-4 && j+offset!=height-5)
                            {
                                fillArea(i,j,i+UnityEngine.Random.Range(2, width/3-2),Min(j+offset,height-1));
                                j=Min(j+offset,height-1);
                                lastHillY=j;
                            }
                            else
                                matrix[i,j]=1;
                        }
                        else
                        {
                            matrix[i,j]=1;
                        }
                    }
                }
                else if (j==0)
                {
                    if (i>=9 && Random.value<0.05 && i!=width/2+(width%2)-1 && i!=width/2+(width%2)-4 && i-lastHillX1>=5)
                    {
                        offset=1;
                        if (i+offset==width/2+(width%2)-3)
                            offset+=1;
                        fillArea(i,j,i+offset,j+UnityEngine.Random.Range(1, height/3-2));
                        i+=offset;
                        lastHillX1=i;
                    }
                    else
                    {
                        matrix[i,j]=1;
                    }
                }
                else if (j==height-1)
                {
                    if (i>=9 && Random.value<0.05 && i!=width/2+(width%2)-1 && i!=width/2+(width%2)-4 && i-lastHillX2>=5)
                    {
                        offset=1;
                        if (i+offset==width/2+(width%2)-3)
                            offset+=1;
                        fillArea(i,j-UnityEngine.Random.Range(1, height/3-2),i+offset,j);
                        i+=offset;
                        lastHillX2=i;
                    }
                    else
                    {
                        matrix[i,j]=1;
                    }
                }
            }
        }
        
        for (int j = 1; j < height-1; j++)
        {
            if (Random.value<0.05 && matrix[0,j-1]!=0)   
            {
                for (int i = 0; i < width/3-1; i++)
                matrix[i,j]=0;
            }
        }
    }

    public bool checkSquare()
    {
        bool square=false;
        for (int i = 1; i < width/2+(width%2); i++)
        {
            for (int j = 1; j < height-2; j++)
            {
                if((matrix[i,j]==0)&&(matrix[i+1,j]==0)&&(matrix[i,j+1]==0)&&(matrix[i+1,j+1]==0))
                {
                    square=true;
                    goto finish;
                }
            }
        }
        finish:
        return(square);
    }

    public void InitializeMatrix()
    {
        // Create a new empty matrix with dimensions width x height
        matrix = new int[width, height];
        map = new int[width, height];
        possibleStates = new HashSet<int>[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                possibleStates[i, j] = new HashSet<int>();
            }
        }

        int test =0;

        restart:

        test+=1;

        resetMatrix();

        generateBorder();

        paintRoad();

        //phantom cage
        for (int i=width/2+(width%2)-3; i<width/2+(width%2); i++)
        {
            for (int j=height/2+(height%2)-2; j<=height/2+(height%2); j++)
            {
                matrix[i, j] = 1;
            }
        }

        int x, y;
        while(true)
        {
            checkState();
            (x, y) = findEmpty();
            if (x==-1)
            {
                break;
            }
            else if (y==-1)
            {   
                if (test>=1000000)
                    {
                        Debug.Log("couldnt find");
                        break;
                    }
                goto restart;
            }
            chooseState(x,y);
            paintRoad();
            ResetStates();
            if (checkSquare())
                goto restart;
        }

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
                if (matrix[i, j] == 1)
                {
                    row += $"<color=blue>{matrix[i, j]}</color> ";
                }
                else
                {
                    row += $"<color=red>{matrix[i, j]}</color> ";
                }
            }
            Debug.LogFormat(row);
        }
    }
}