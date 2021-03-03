---
author: Bilal Guillaumie
date: 03-03-21
subject: Task asyn note
---

**TAP:** Task asynchronous programming model : https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/task-asynchronous-programming-model  



Practices :
- Method signature -> `async`
- Return type `Task<>`
- Method name must finished with `Async`

The return type is one of the following types: 

- `Task<TResult>` if your method has a return statement in which the operand has type TResult.
- `Task` if your method has no return statement or has a return statement with no operand.
- `void` if you're writing an async event handler.
- Any other type that has a GetAwaiter method (starting with C# 7.0).


