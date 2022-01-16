Imports System

Module Module1
    Sub Main()
        'Declare 2 matrices to keep track of past guesses
        Dim allSolutions(4, 10) As Integer
        Dim allResults(2, 10) As Integer
        
        'Declare an array for the solution set the user will try to guess       
        Dim solution As Integer() = {6, 6, 6, 6}

        Dim random As New Random
        
        Dim counter As Integer
        Dim otherCounter As Integer
        
        'Generate 4 random values from 1 to 6 that will comprise the solution
        For counter = 0 To 3
random:
            solution(counter) = random.Next(1, 7)
            'Once a random value is generated, iterate over past random value to ensure it is not a repeated value
            'If the value is repeated, generate a new value
            For otherCounter = 0 To (counter - 1)
                If solution(counter) = solution(otherCounter) Then
                    GoTo random
                End If
            Next
        Next
        
        Dim turn As Integer = 0
        
        'Display the set of possible values for the player
        Console.WriteLine("Possible numbers in solution:")
        For number = 0 To 5
            Console.WriteLine($"{number + 1}")
        Next
        Console.WriteLine()
 
        'For 10 turns
        While turn < 10
            'Print an example to help the user visualize the game and the 4 spots 
            Console.WriteLine("Spot #:            1  2  3  4
Solution (hidden): *  *  *  *")
            Console.WriteLine()
            
            'Print all previous guesses as well as the number of exact and non-perfect matches they had by referring to their respective matrix
            Console.WriteLine("Previous guesses:")
            Console.WriteLine()
            For turnNumber As Integer = 0 To turn - 1
                Console.WriteLine($"Guess number {turnNumber + 1}: {allSolutions(0, turnNumber)} {allSolutions(1, turnNumber)} {allSolutions(2, turnNumber)} {allSolutions(3, turnNumber)}")
                Console.WriteLine($"Perfect guesses: {allResults(0, turnNumber)}
Non-perfect guesses: {allResults(1, turnNumber)}")
                Console.WriteLine()
            Next
 
            'Print the number of turns left
            Console.Write("Turns left: ")
            Console.WriteLine(10 - turn)
            Console.WriteLine()
 
            'Declare an array to store the user's entire guess and a string to hold each specific entry 
            Dim guesses As Integer() = {6, 6, 6, 6}
            Dim guess As String
 
            Dim newCounter As Integer
            
            'Loop to take 4 inputs from the user for the 4 guesses they need to make
            For counter = 0 To 3
retry:
                'Prompt the user to enter their guess for each spot and store their input
                Console.WriteLine($"Please enter your guess for Spot # {counter + 1}")
                guess = Console.ReadLine()
                
                'Try casting their entry as an integer
                Try
                    guess = CType(guess, Integer)
                    'If successful, store their type-casted guess in the appropriate spot in the array
                    guesses(counter) = guess
                Catch ex As Exception
                    'If unsuccessful, notify the user and allow them to retry until successful
                    Console.WriteLine("Sorry, your input is invalid (non-integer entry)")
                    GoTo retry
                End Try
                
                'Since all the values of the solution must be different, ensure that all values of the guess are also different
                For newCounter = 0 To counter - 1
                    If (guesses(counter) = guesses(newCounter)) Then
                        Console.WriteLine("Sorry, you have already guessed that value, and there can only be one of each value")
                        GoTo retry
                    End If
                Next
            Next
 
            
            counter = 0
            Dim matches As Integer = 0
            Dim exactMatches As Integer = 0
            
            'Iterate over the array containing the guess and compare each element to the array containing the solution
            For Each value As Integer In guesses
                otherCounter = 0
                For Each otherValue As Integer In solution
                    If (value = otherValue) Then
                        'If the values and the positions within the array are identical, increment the exact match counter
                        If (counter = otherCounter) Then
                            exactMatches += 1
                            Exit For
                        'If the values are identical but the positions within the array do not match, increment the match counter
                        Else
                            matches += 1
                            Exit For
                        End If
                    End If
                    otherCounter += 1
                Next
                counter += 1
            Next
            
            'Notify the player of the results
            Console.WriteLine()
            Console.WriteLine($"Amount of exact matches: {exactMatches}
Amount of non-perfect matches: {matches}")
 
            'Store all values of the guesses in the matrix containing all past guesses
            For differentCounter As Integer = 0 To 3
                allSolutions(differentCounter, turn) = guesses(differentCounter)
            Next
            'Store the results of checking for exact and non-perfect matches in the matrix containing past results  
            allResults(0, turn) = exactMatches
            allResults(1, turn) = matches
           
            turn += 1
            
            Console.WriteLine()
            
            'If the most recent guess has 4 exact matches, meaning every guess is in the right spot, notify the user that they have won the game and end the game
            If exactMatches = 4 Then
                Console.WriteLine($"Congratulations!! You broke the code with {10 - turn} turn(s) left to spare! The solution was:")
                For Each answer In solution
                    Console.Write($"{answer} ")
                Next
                Exit Sub
            End If
            
input:
            'Check if the user is ready to proceed to the next turn, which allows them time to process the information given by the game thus far
            'Convert all inputs to lowercase so that their entry can be valid regardless of case
            Dim input As String
            Console.WriteLine("Are you ready to proceed to the next turn? (Y/N)")
            input = Console.ReadLine
            
            'If yes, proceed as normal (do nothing)
            If input.ToLower() = "y" Then
                
            'If no, confirm if the user wants to end the game
            ElseIf input.ToLower() = "n" Then
                Console.WriteLine("Okay, do you wish to exit the game? (Y/N)")
                input = Console.ReadLine()
                
                'If yes, quit game
                If input.ToLower = "y" Then
                    Exit Sub
                    
                'If no, revert back to initial question 
                ElseIf input.ToLower() = "n" Then
                    GoTo input
                    
                'If another entry (invalid entry) notify the user and allow them to try again
                Else 
                    Console.WriteLine("Sorry, that was an invalid input (Not Y or N). Please try again")
                    GoTo input
                End If
            Else
                Console.WriteLine("Sorry, that was an invalid input (Not Y or N). Please try again")
                GoTo input
            End If
            
        End While
 
        'If the game reaches this point, the while loop terminated due to 10 turns being completed, meaning the user has lost the game
        'Notify the user and display the correct solution
        If turn = 10 Then
            Console.WriteLine("Sorry, that was your last turn. You lost the game :(")
            Console.Write("The correct solution was: ")
            For Each answer In solution
                Console.Write($"{answer} ")
            Next
        End If
        
        'Delay added so that the program does not terminate immediately and the user can read the output before the game ends
        For delay = 1 To 10000000000
        
        Next
    End Sub
End Module
