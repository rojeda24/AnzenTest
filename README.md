# How to read my code
My code was made using .NET Core 8.0 implementing an Object-Relational Mapping (Entity Framework), Models, a repository pattern using DbSet. Is connected to a SQLite database with the name **satori.db** that is loaded with dummy data. The database was auto-generated using Migrations and LINQ.

Every new commit is related with each implementation, fix or refactor I did. Is a good way to check my progress.

Most of the logic is in **Program.cs** in a minimalAPI (avoiding Controllers because of the simplicity of the code).

I created a **Model** for each table, to define attributes, some of the relationships and to handle data. All of them inside **Models/** directory.

Inside Data/**SatoriContext.cs** you can find the database session and some configuration with the relationships between.

Thanks for your time. Have an amazing new year!

# Anzen API Documentation (With the help of Swagger and LLMs like Copilot 🤪)

**API Version:** 1.0

**Base URL:** http://localhost:5224

**Endpoints:**

##**1. GET /submissions**

**Description:** Retrieves a list of submissions.

**Parameters:**

* **page** (integer, optional, default=1): The page number to retrieve.
* **pageSize** (integer, optional, default=10): The number of submissions per page.
* **columnToSort** (string, optional, default="Id"): The column to sort by.
    * Possible string values:
        * **Id**
        * **AccountName**
        * **UwName**
        * **EffectiveDate**
        * **ExpirationDate**
        * **Sic**
* **asc** (boolean, optional, default=true): Whether to sort in ascending or descending order.

**Response:**

* **Status:** 200 (Success)
* **Body:** An array of submissions, each containing the following fields:
    * id
    * accountName
    * uwName
    * premium
    * effectiveDate
    * expirationDate
    * sic
    * status (status with key and name field. In database: 1=New, 2=In Progress 3=Done)
    * coverages (list of objects with id and name fields)

**Example Request:**

```bash
curl -X GET 'http://localhost:5224/submissions?page=2&pageSize=2&column=Id&asc=true' -H 'accept: */*'
```

**Example Response:**

```json
[
  {
    "id": 1,
    "accountName": "ACME Technologies",
    "uwName": "a Floyd Miles 2nd",
    "premium": 12000,
    "effectiveDate": "2023-02-02",
    "expirationDate": "2023-02-02",
    "sic": "01011 Iron Ores",
    "status": {
      "id": 1,
      "name": "New"
    },
    "coverages": [
      {
        "id": 1,
        "name": "EPLI"
      },
      {
        "id": 2,
        "name": "D&O"
      }
    ]
  },
  {
    "id": 2,
    "accountName": "ZenithWay",
    "uwName": "Floyd Miles",
    "premium": 10000,
    "effectiveDate": "2023-03-03",
    "expirationDate": "2024-03-03",
    "sic": "01012 Dummy Industry",
    "status": {
      "id": 2,
      "name": "In Progress"
    },
    "coverages": []
  }
]
```

## **2. GET /submissions/{id}**

**Description:** Retrieves a specific submission by its ID.

**Parameters:**

* **id** (string): The ID of the submission to retrieve.

**Response:**

* **Status:** 200 (Success)
* **Body:** The submission object with the following fields:
    * id
    * accountName
    * uwName
    * premium
    * effectiveDate
    * expirationDate
    * sic
    * status (status with key and name field. In database: 1=New, 2=In Progress 3=Done)
    * coverages (list of objects with id and name fields)

**Example Request:**

```bash
curl -X GET 'http://localhost:5224/search/Floyd%20Miles' -H 'accept: */*'
```

**Example Response:**

```json
{
  "id": 1,
  "accountName": "ACME Technologies",
  "uwName": "a Floyd Miles 2nd",
  "premium": 12000,
  "effectiveDate": "2023-02-02",
  "expirationDate": "2023-02-02",
  "sic": "01011 Iron Ores",
  "status": {
    "id": 1,
    "name": "New"
  },
  "coverages": [
    {
      "id": 1,
      "name": "EPLI"
    },
    {
      "id": 2,
      "name": "D&O"
    }
  ]
}
```
Based on the fragment of the `swagger.json` you provided, it seems like you've added a new endpoint for searching submissions. Here's how you can document this new endpoint in your `README.MD`:

## **3. GET /search/{search}**

**Description:** Searches submissions based on the provided search term.
    * Submission information checked:
        * **AccountName**
        * **UwName**
        * **EffectiveDate**
        * **ExpirationDate**
        * **Sic** 
        * **Premium💸** 

**Parameters:**

* **search** (string, required): The search term to use for finding submissions.
* **page** (integer, optional, default=1): The page number to retrieve.
* **pageSize** (integer, optional, default=10): The number of submissions per page.

**Response:**

* **Status:** 200 (Success)
* **Body:** An array of submissions that match the search term, each containing the following fields:
    * id
    * accountName
    * uwName
    * premium
    * effectiveDate
    * expirationDate
    * sic
    * status (status with key and name field. In database: 1=New, 2=In Progress 3=Done)
    * coverages (list of objects with id and name fields)

**Example Request:**

```bash
curl -X GET 'http://localhost:5224/search/{search}?page=1&pageSize=10' -H 'accept: */*'
```

**Example Response:**

```json
[
  {
    "id": 1,
    "accountName": "ACME Technologies",
    "uwName": "a Floyd Miles 2nd",
    "premium": 12000,
    "effectiveDate": "2023-02-02",
    "expirationDate": "2023-02-02",
    "sic": "01011 Iron Ores",
    "status": {
      "id": 1,
      "name": "New"
    },
    "coverages": [
      {
        "id": 1,
        "name": "EPLI"
      },
      {
        "id": 2,
        "name": "D&O"
      }
    ]
  },
  {
    "id": 2,
    "accountName": "ZenithWay",
    "uwName": "Floyd Miles",
    "premium": 10000,
    "effectiveDate": "2023-03-03",
    "expirationDate": "2024-03-03",
    "sic": "01012 Dummy Industry",
    "status": {
      "id": 2,
      "name": "In Progress"
    },
    "coverages": []
  }
]
```
