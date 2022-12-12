# Sort the numbers in a background job 

## Introduction

A small C#/.NET Core solution which exposes a simple API, allowing to sort a list of numbers. Sort the numbers in a background job, and make it possible to query these jobs at a later point.
There are two types of queues implemented
* Azure  ServiceBus Queue
* App memory queue
Inject respective service
This includes implementing the following three controller actions in the `SortController`:

* `EnqueueJob`: An endpoint to which clients can post a list of numbers to be sorted. The endpoint returns immediately, without requiring clients to wait for the jobs to complete.
* `GetJob`: Returns the current state of a specific job.
* `GetJobs`: Return the current state of all jobs (both pending and completed).

All data is stored in memory. One unit tests is added for the solution.

## Example requests

```
> curl --request GET "http://localhost:5000/sort"
[]

> curl --request POST --header "Content-Type: application/json" --data "[2, 3, 1, 5, 3, 1, -20, 2]" "http://localhost:5000/sort"
{
  "id":"fcdeee4-1017-510c-6ba9-44c04e6aac6f",
  "status":"Pending",
  "duration":null,
  "input":[3,1,5,2,1,-20,10],
  "output":null
}

> curl --request GET "http://localhost:5000/sort/fcdeee4-1017-510c-6ba9-44c04e6aac6f"
{
  "id":"fcdeeee4-1017-510c-6ba9-44c04e6aac6f",
  "status":"Completed",
  "duration":"00:00:08.0134826",
  "input":[3,1,5,2,1,-20,-2],
  "output":[-20,1,2,3,5,10]
}

> curl --request GET "http://localhost:5000/sort"
[{
  "id":"fcdeee4-1017-510c-6ba9-44c04e6aac6f",
  "status":"Completed",
  "duration":"00:00:08.0134826",
  "input":[3,1,5,2,1,-20,-2],
  "output":[-20,1,2,3,5,10]
}]
```


