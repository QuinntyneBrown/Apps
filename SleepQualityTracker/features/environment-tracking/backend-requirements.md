# Backend Requirements - Environment Tracking

## API Endpoints

### POST /api/environment
Log environment conditions
- **Request Body**: sessionId, temperature, noiseLevel, lightLevel, humidity, userId
- **Events**: `SleepEnvironmentLogged`

### GET /api/environment/{sessionId}
Get environment for session

### POST /api/environment/analyze/{userId}
Analyze optimal conditions
- **Events**: `OptimalEnvironmentIdentified`, `SuboptimalConditionsDetected`

### GET /api/environment/optimal/{userId}
Get user's optimal conditions

## Domain Models
### SleepEnvironment: Id, SessionId, Temperature, NoiseLevel, LightLevel, Humidity, LoggedAt
### OptimalEnvironment: Id, UserId, OptimalTempRange, OptimalNoiseLevel, OptimalLightLevel

## Business Logic
- Correlate environment with sleep quality
- Identify optimal ranges per user (require 21+ days data)
- Detect suboptimal conditions: temp outside 60-70°F, noise >40dB, light >10 lux
- Support smart home device integration

## Events
SleepEnvironmentLogged, OptimalEnvironmentIdentified, SuboptimalConditionsDetected

## Database Schema
SleepEnvironments table, OptimalEnvironments table
