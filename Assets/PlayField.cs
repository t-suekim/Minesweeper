using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayField
{
    // The Grid itself
    public static int w = 10; 
    public static int h = 10;
    public static Element[,] elements = new Element[w, h];
    public static double density = 0.05;
    public static int score;
    public static int resetBoard = 0;
    public static string status;
    public static string enemyStatus;
    public static bool isOpened;
    public static bool gameOver = false;

    // Uncover all Mines
    public static void uncoverMines()
    {
        foreach (Element elem in elements)
        {
            if (elem.mine) elem.loadTexture(0);
        }
        PlayField.isOpened = true;
    }

    public static void sendAction(int time)
    {
        // TODO sending interactions to enemy
        enemyStatus = "Affected";
    }

    public static bool isFinished()
    {
        // Try to find a covered element that is no mine
        foreach (Element elem in elements)
            if (elem.isCovered() && !elem.mine)
                return false;
        // There are none => all are mines => game won.
        gameOver = true;
        return true;
    }

    

    // Find out if a mine is at the coordinates
    public static bool mineAt(int x, int y)
    {
        // Coordinates in range? Then check for mine.
        if (x >= 0 && y >= 0 && x < w && y < h)
            return elements[x, y].mine;
        return false;
    }

    // Count adjacent mines for an element
    public static int adjacentMines(int x, int y)
    {
        int count = 0;

        if (mineAt(x, y + 1)) ++count; // top
        if (mineAt(x + 1, y + 1)) ++count; // top-right
        if (mineAt(x + 1, y)) ++count; // right
        if (mineAt(x + 1, y - 1)) ++count; // bottom-right
        if (mineAt(x, y - 1)) ++count; // bottom
        if (mineAt(x - 1, y - 1)) ++count; // bottom-left
        if (mineAt(x - 1, y)) ++count; // left
        if (mineAt(x - 1, y + 1)) ++count; // top-left

        return count;
    }

    // Flood Fill empty elements
    public static void FFuncover(int x, int y, bool[,] visited)
    {
        // Coordinates in Range?
        if (x >= 0 && y >= 0 && x < w && y < h)
        {
            // visited already?
            if (visited[x, y])
                return;

            // uncover element
            elements[x, y].loadTexture(adjacentMines(x, y));
            
            // close to a mine? then no more work needed here
            if (adjacentMines(x, y) > 0)
                return;

            // set visited flag
            visited[x, y] = true;

            // recursion
            FFuncover(x - 1, y - 1, visited);
            FFuncover(x, y - 1, visited);
            FFuncover(x + 1, y - 1, visited);
            FFuncover(x - 1, y, visited);
            FFuncover(x + 1, y, visited);
            FFuncover(x - 1, y + 1, visited);
            FFuncover(x, y + 1, visited);
            FFuncover(x + 1, y + 1, visited);
        }
    }

}
