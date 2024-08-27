# Task Tracker CLI

## Overview

Task Tracker CLI is a command-line application built with .NET 8 to manage and track tasks efficiently. It allows users to add, update, delete, and list tasks, as well as mark tasks as in progress or completed. The tasks are stored in a JSON file, which is created automatically if it does not exist.

Requirements: https://roadmap.sh/projects/task-tracker

## Features

- **Add a new task**: Add a task with a description.
- **Update an existing task**: Modify the description of a task.
- **Delete a task**: Remove a task by its ID.
- **Mark tasks as in progress or done**: Update the status of a task.
- **List all tasks**: View all tasks.
- **List tasks by status**: Filter tasks based on their status (`todo`, `in-progress`, `done`).

## Task Properties

Each task has the following properties:
- `id`: Unique identifier for the task.
- `description`: A brief description of the task.
- `status`: The status of the task (`todo`, `in-progress`, `done`).
- `createdAt`: The date and time when the task was created.
- `updatedAt`: The date and time when the task was last updated.

## Getting Started

### Prerequisites

- .NET 8 SDK installed on your machine.
- A code editor or IDE (e.g., Visual Studio Code, Visual Studio).

### Setup

1. **Clone the Repository**

   ```bash
   git clone https://github.com/TuanKha-Dev-Fullstack/Task-Tracer-CLI.git
   cd Task-Tracer-CLI
   ```

2. **Build the Project**

    ```bash
    dotnet build
    ```
    
3. **Run the Application**

    ```bash
    dotnet run
    ```

### Usage

- **Add a new task**

  ```bash
  add "<description>"
  ```
  
- **Update a task**

  ```bash
  update <ID> "<New description>"
  ```
  
- **Delete a task**

  ```bash
  delete <ID>
  ```

- **Mark a task as in progress**

  ```bash
  mark-in-progress <ID>
  ```

- **Mark a task as done**

  ```bash
  mark-done <ID>
  ```

- **List all tasks**

  ```bash
  list
  ```

- **List tasks by status (todo, in-progress, done)**

  ```bash
  list <status>
  ```

- **Help**

  ```bash
  help
  ```

- **Exit program**

  ```bash
  exit
  ```
