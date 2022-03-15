# gradesAnalysis
### Objective 
Analyzes of course details, including calculations of average, variance and standard deviation of selected courses.

### Usage
1. Enter all your courses' details, including: Name, Grade, Points (credit hours), Year, Semester and Department. (takes some time, but don't worry - it saves the data in XML file so you will only need to enter the data once)
2. Select the operation (as average, variance etc.)
3. State whether to use all the courses, or specific courses using realtime-compiled Predicates. For example, if you want to get the average of courses taken in the first semester of the second year, the condition is: `c -> c.Year == 2 && c.Semester == 1`
4. Get the result :)
