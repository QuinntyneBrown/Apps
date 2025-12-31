# User Management Software Requirements Specification

## 1. Introduction

### 1.1 Purpose
This document specifies the software requirements for the User Management module of the TaxDeductionOrganizer system. This module provides functionality for creating, updating, reading, and deleting users and roles.

### 1.2 Scope
The User Management module enables administrators to manage system users and their roles. It includes full CRUD operations for both users and roles, with the ability to assign and remove roles from users.

### 1.3 Definitions
- **User**: An entity representing a person who can access the system
- **Role**: A named permission set that can be assigned to users
- **Admin**: A user with administrative privileges to manage other users and roles

## 2. Functional Requirements

### 2.1 User Management

#### 2.1.1 Create User
- **FR-UM-001**: The system shall allow administrators to create new users
- **FR-UM-002**: A user shall have the following required attributes:
  - Username (unique, non-empty)
  - Email (unique, valid email format)
  - Password (minimum 8 characters)
- **FR-UM-003**: Upon creation, the password shall be hashed with a unique salt

#### 2.1.2 Read Users
- **FR-UM-004**: The system shall provide an endpoint to retrieve all users
- **FR-UM-005**: The system shall provide an endpoint to retrieve a single user by ID
- **FR-UM-006**: User passwords shall never be returned in API responses

#### 2.1.3 Update User
- **FR-UM-007**: The system shall allow updating user profile information
- **FR-UM-008**: Email changes shall validate uniqueness
- **FR-UM-009**: Password changes shall re-hash with a new salt

#### 2.1.4 Delete User
- **FR-UM-010**: The system shall allow administrators to delete users
- **FR-UM-011**: Deletion shall be a hard delete from the database

### 2.2 Role Management

#### 2.2.1 Create Role
- **FR-RM-001**: The system shall allow administrators to create new roles
- **FR-RM-002**: A role shall have a unique name

#### 2.2.2 Read Roles
- **FR-RM-003**: The system shall provide an endpoint to retrieve all roles
- **FR-RM-004**: The system shall provide an endpoint to retrieve a single role by ID

#### 2.2.3 Update Role
- **FR-RM-005**: The system shall allow updating role names
- **FR-RM-006**: Role name changes shall validate uniqueness

#### 2.2.4 Delete Role
- **FR-RM-007**: The system shall allow administrators to delete roles
- **FR-RM-008**: Role deletion shall remove the role from all associated users

### 2.3 User-Role Association

#### 2.3.1 Add Role to User
- **FR-URA-001**: The system shall allow adding roles to a user
- **FR-URA-002**: A user can have multiple roles
- **FR-URA-003**: Adding a duplicate role shall be idempotent

#### 2.3.2 Remove Role from User
- **FR-URA-004**: The system shall allow removing roles from a user
- **FR-URA-005**: Removing a non-existent role shall not cause an error

## 3. API Endpoints

### 3.1 Users Controller

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/users | Get all users |
| GET | /api/users/{userId} | Get user by ID |
| POST | /api/users | Create new user |
| PUT | /api/users/{userId} | Update user |
| DELETE | /api/users/{userId} | Delete user |
| POST | /api/users/{userId}/roles | Add role to user |
| DELETE | /api/users/{userId}/roles/{roleId} | Remove role from user |

### 3.2 Roles Controller

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/roles | Get all roles |
| GET | /api/roles/{roleId} | Get role by ID |
| POST | /api/roles | Create new role |
| PUT | /api/roles/{roleId} | Update role |
| DELETE | /api/roles/{roleId} | Delete role |

## 4. Data Models

### 4.1 User Entity
```csharp
public class User
{
    public Guid UserId { get; }
    public string UserName { get; }
    public string Email { get; }
    public string Password { get; }  // Hashed
    public byte[] Salt { get; }
    public List<Role> Roles { get; }
}
```

### 4.2 Role Entity
```csharp
public class Role
{
    public Guid RoleId { get; }
    public string Name { get; }
}
```

### 4.3 UserRole Join Entity
```csharp
public class UserRole
{
    public Guid UserId { get; }
    public Guid RoleId { get; }
}
```

## 5. Non-Functional Requirements

### 5.1 Security
- **NFR-SEC-001**: Passwords shall be hashed using a secure algorithm (PBKDF2, bcrypt, or Argon2)
- **NFR-SEC-002**: All user management endpoints shall require authentication
- **NFR-SEC-003**: Only users with Admin role can manage users and roles

### 5.2 Performance
- **NFR-PERF-001**: User list queries shall return within 500ms for up to 1000 users
- **NFR-PERF-002**: Database indices shall be created on frequently queried columns

### 5.3 Data Integrity
- **NFR-DI-001**: Email and username uniqueness shall be enforced at the database level
- **NFR-DI-002**: Role name uniqueness shall be enforced at the database level
