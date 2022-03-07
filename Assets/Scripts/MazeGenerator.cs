using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Flags]

// For this part I followed a youtube tutorial for recursive backtracker algorithm
// This is the link https://www.youtube.com/watch?v=ya1HyptE5uc&t=1079s


// WallState object represents a cell in the maze.
// It is a number to record what walls does that cell have.
// It is designed as follows.
public enum WallState
{
   
    LEFT = 1, // 0001
    RIGHT = 2, // 0010
    UP = 4, // 0100
    DOWN = 8, // 1000

    VISITED = 128, // 1000 0000

}

public struct Position
{
    public int X;
    public int Y;
}

public struct Neighbour
{
    public Position Position;    // Position of the Neighbor
    public WallState SharedWall; // The shared wall of current cell and the neighbor
}

public static class MazeGenerator
{

    private static WallState GetOppositeWall(WallState wall)
    {
        switch (wall)
        {
            case WallState.RIGHT: return WallState.LEFT;
            case WallState.LEFT: return WallState.RIGHT;
            case WallState.UP: return WallState.DOWN;
            case WallState.DOWN: return WallState.UP;
            default: return WallState.LEFT;
        }
    }

    private static WallState[,] ApplyRecursiveBacktracker(WallState[,] maze, int width, int height)
    {
        // Before this we set the all the maze cells with 4 walls.
        // Randomly choose a cell as the starting point
        var rng = new System.Random(/*seed*/);   
        var position = new Position { X = rng.Next(0, width), Y = rng.Next(0, height) };

        
        // Set the starting point as visited and add it to the stack.
        // Randomly choose one of its unvisited neighbor
        // and mofidy the WallState so that they become a "path".
        // Then the neighbor becomes current cell. And by repeating we
        // arrive an end point where all the neighbors are visited. This is a good path in the maze.

        // Then, poping more cells in the stack, we actually go backward along the path.
        // We find the remained unvisited neighbors and push them in the stack.
        // Repeatedly doing that, when the stack is empty, the maze is completed.
        var positionStack = new Stack<Position>();       
        maze[position.X, position.Y] |= WallState.VISITED;  
        positionStack.Push(position);

        while (positionStack.Count > 0)
        {
            var current = positionStack.Pop();
            var neighbours = GetUnvisitedNeighbours(current, maze, width, height);

            if (neighbours.Count > 0)
            {
                positionStack.Push(current);

                var randIndex = rng.Next(0, neighbours.Count);
                var randomNeighbour = neighbours[randIndex];

                var nPosition = randomNeighbour.Position;
                maze[current.X, current.Y] &= ~randomNeighbour.SharedWall;
                maze[nPosition.X, nPosition.Y] &= ~GetOppositeWall(randomNeighbour.SharedWall);
                maze[nPosition.X, nPosition.Y] |= WallState.VISITED;

                positionStack.Push(nPosition);
            }
        }

        return maze;
    }

    // This method looks for the unvisited neighbours of a cell.
    // if a neighbour is unvisited, we will initiate a new Neighbour and put it in the list.
    // Then it returns this list.
    private static List<Neighbour> GetUnvisitedNeighbours(Position p, WallState[,] maze, int width, int height)
    {
        var list = new List<Neighbour>();

        if (p.X > 0) // left neighbour
        {
            if (!maze[p.X - 1, p.Y].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        X = p.X - 1,
                        Y = p.Y
                    },
                    SharedWall = WallState.LEFT
                });
            }
        }

        if (p.Y > 0) // Down neighbour
        {
            if (!maze[p.X, p.Y - 1].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        X = p.X,
                        Y = p.Y - 1
                    },
                    SharedWall = WallState.DOWN
                });
            }
        }

        if (p.Y < height - 1) // Up neighbour
        {
            if (!maze[p.X, p.Y + 1].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        X = p.X,
                        Y = p.Y + 1
                    },
                    SharedWall = WallState.UP
                });
            }
        }

        if (p.X < width - 1) // Right neighbour
        {
            if (!maze[p.X + 1, p.Y].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        X = p.X + 1,
                        Y = p.Y
                    },
                    SharedWall = WallState.RIGHT
                });
            }
        }

        return list;
    }

    public static WallState[,] Generate(int width, int height)
    {
        //At first, all the cells in the maze has 4 walls
        //And then we apply the recursive backtracker to modify it to a maze.

        WallState[,] maze = new WallState[width, height];
        WallState initial = WallState.RIGHT | WallState.LEFT | WallState.UP | WallState.DOWN;
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                maze[i, j] = initial;  // 1111
            }
        }
        return ApplyRecursiveBacktracker(maze, width, height);
    }
}