# Identity Specification

## 1. Introduction

### 1.1 Purpose
This document specifies the identity and authentication requirements for the AnniversaryBirthdayReminder system. It defines how users authenticate, how JWT tokens are generated and validated, and how role-based access control is implemented.

### 1.2 Scope
The identity system covers:
- User authentication via username/password
- JWT token generation with claims
- Role-based authorization
- Login page in the admin application

## 2. Authentication

### 2.1 Login Flow
1. User submits credentials (username and password)
2. System validates credentials against stored hash
3. If valid, system generates JWT token with user claims
4. Token is returned to client
5. Client stores token and includes in subsequent requests

### 2.2 Password Hashing
- **Algorithm**: PBKDF2 with SHA-256
- **Salt**: Unique 32-byte salt per user
- **Iterations**: 100,000 minimum

### 2.3 Login Endpoint

#### Request
```http
POST /api/auth/login
Content-Type: application/json

{
    "username": "string",
    "password": "string"
}
```

#### Response (Success)
```http
HTTP/1.1 200 OK
Content-Type: application/json

{
    "token": "eyJhbGciOiJIUzI1NiIs...",
    "expiresAt": "2024-01-15T12:00:00Z",
    "user": {
        "userId": "guid",
        "userName": "string",
        "email": "string",
        "roles": ["Admin", "User"]
    }
}
```

#### Response (Failure)
```http
HTTP/1.1 401 Unauthorized
Content-Type: application/json

{
    "error": "Invalid username or password"
}
```

## 3. JWT Token

### 3.1 Token Structure

#### Header
```json
{
    "alg": "HS256",
    "typ": "JWT"
}
```

#### Payload (Claims)
```json
{
    "sub": "user-id-guid",
    "name": "username",
    "email": "user@example.com",
    "roles": ["Admin", "User"],
    "iat": 1704067200,
    "exp": 1704153600,
    "iss": "AnniversaryBirthdayReminder",
    "aud": "AnniversaryBirthdayReminder.Api"
}
```

### 3.2 Token Configuration
- **Expiration**: 24 hours
- **Signing Algorithm**: HS256
- **Secret Key**: Configured in appsettings.json (minimum 32 characters)

### 3.3 Claims
| Claim | Description |
|-------|-------------|
| sub | User ID (GUID) |
| name | Username |
| email | User email address |
| roles | Array of role names |
| iat | Issued at timestamp |
| exp | Expiration timestamp |
| iss | Token issuer |
| aud | Token audience |

## 4. Authorization

### 4.1 Role-Based Access Control
The system implements role-based access control (RBAC) with the following predefined roles:

| Role | Description |
|------|-------------|
| Admin | Full system access, can manage users and roles |
| User | Standard user access |

### 4.2 Protected Endpoints
All API endpoints except `/api/auth/login` require authentication.

| Endpoint Pattern | Required Role |
|-----------------|---------------|
| /api/users/* | Admin |
| /api/roles/* | Admin |
| /api/* | User or Admin |

### 4.3 Authorization Header
```http
Authorization: Bearer eyJhbGciOiJIUzI1NiIs...
```

## 5. Configuration

### 5.1 JWT Settings (appsettings.json)
```json
{
    "Jwt": {
        "Key": "YourSuperSecretKeyAtLeast32Characters",
        "Issuer": "AnniversaryBirthdayReminder",
        "Audience": "AnniversaryBirthdayReminder.Api",
        "ExpiresInHours": 24
    }
}
```

## 6. Admin Login Page

### 6.1 Features
- Username and password input fields
- Form validation
- Error message display for invalid credentials
- Redirect to dashboard upon successful login
- "Remember me" option (optional)

### 6.2 UI Components
- Material Design form fields
- Loading indicator during authentication
- Clear error feedback

### 6.3 Security Measures
- Rate limiting on login attempts (optional future enhancement)
- HTTPS required
- Token storage in localStorage or sessionStorage

## 7. Seeding

### 7.1 Default Admin User
On first application startup, the system shall seed a default admin user:

| Field | Value |
|-------|-------|
| Username | admin |
| Email | admin@anniversary-birthday-reminder.local |
| Password | Admin123! |
| Roles | Admin |

### 7.2 Default Roles
The following roles shall be seeded:

| Role Name | Description |
|-----------|-------------|
| Admin | Full system access |
| User | Standard user access |

## 8. Implementation Notes

### 8.1 Backend Implementation
- Use ASP.NET Core JWT Bearer authentication
- Implement custom password hashing service
- Create JWT token generation service
- Configure authentication middleware in Program.cs

### 8.2 Frontend Implementation
- Create Angular login component
- Implement auth service with token management
- Add HTTP interceptor for automatic token injection
- Implement auth guard for protected routes
