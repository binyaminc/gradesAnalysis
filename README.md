# gradesAnalysis
## Objective 
Analysis of course details, including calculations of average, variance and standard deviation of the selected courses.

## Usage
1. Enter all your courses' details, where each course contains Name, Grade, Points (credit hours), Year, Semester and Department. (takes some time, but don't worry - it saves the data in XML file so you will only need to enter the data once)
2. Select the operation (as average, variance etc.)
3. State whether to use all the courses, or specific courses using realtime-compiled Predicates. For example, if you want to get the average of courses taken in the first year in math department, with grade grater than 85, the condition is: `c -> c.Year == 1 && c.Dep == "math" && c.Grade >= 85`
4. Get the result :)
