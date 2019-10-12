# Conway's Game of Life
 
1. Any live cell with fewer than two live neighbours dies, as if by underpopulation.
1. Any live cell with two or three live neighbours lives on to the next generation.
1. Any live cell with more than three live neighbours dies, as if by overpopulation.
1. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.

## Usage

`git clone` the directory, then use `dotnet run <grid width> <grid height>` inside it to run. Requires .NET Core 2.2.

## Todo

* Provide better handling/more options for command line arguments.
* Some other expansion - male/female cells or something?
* Encapsulate cells into their own class, not raw `bool`s (for purposes such as the previous bullet)

## Notes

The game of life is a pretty simple exercise I always see thrown around, but I've never gotten around to trying it myself, so it seemed like a fun brainteaser to procrastinate with on a Saturday evening. This is basically a cleanroom implementation - I looked at the rules on wikipedia (as listed above) and went from there in terms of design. 

I'll be honest and say that I'm not too happy with how this one came out. The reason I chose C# (other than that I thought it'd be nice to write it for something other than class) is because of how soothing to the soul it is to chain Linq methods. None of the IEnumerable methods are implemented by 2D arrays however, which means there's a lot of un-idiomatic-feeling `for` loops etc. The class/function structure also feel weird, even though there's nothing drastic I could point to refactoring; like it's a local maxima that's kinda meh overall.
