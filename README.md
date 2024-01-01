 **Anzen API Documentation**

**API Version:** 1.0

**Base URL:** http://localhost:5224

**Endpoints:**

**1. GET /submissions**

**Description:** Retrieves a list of submissions.

**Parameters:**

* **page** (integer, optional, default=1): The page number to retrieve.
* **pageSize** (integer, optional, default=10): The number of submissions per page.
* **column** (string, optional, default="Id"): The column to sort by.
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

**2. GET /submissions/{id}**

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
curl -X GET 'http://localhost:5224/submissions/10' -H 'accept: */*'
```

**Example Response:**

```json
{
  "id": 10,
  "accountName": "ACME Technologies 9",
  "uwName": "Floyd Miles 9",
  "premium": 20000,
  "effectiveDate": "2023-10-10",
  "expirationDate": "2023-10-10",
  "sic": "01019 Iron Ores",
  "status": {
    "id": 1,
    "name": "New"
  },
  "coverages": [{
        "id": 1,
        "name": "EPLI"
      },
      {
        "id": 2,
        "name": "D&O"
  }]
}
```
