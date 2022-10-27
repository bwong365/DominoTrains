# Domino Trains Implementation

**Thanks for taking a look at this project!**

I've included some notes below, it's mostly just my ramblings as I take a break from all the coding, but if you want to read them, they're there!

### Notes

- The tests which cover the actual API usage and gameplay are found in the integrations test project
- I believe Clean architecture was quite appropriate here, especially for a game which is naturally more domain driven
  - Clean ends itself to better abstraction of the domain and therefore easier testing of the business logic
  - I am fond of n-tier for data-driven projects/OLAP projects though
- For this project, I opted to go with a simple global error handling solution via exception-catching middleware. Throwing exceptions is often seen as a poor practice these days, being labeled "glorified go-to statements" and having lower performance, but for the MVP of a single player game, I think the global exception handling is a decent solution. I do like the approach of using discriminated unions, and had thought about borrowing an implementation from one of my other personal projects.
