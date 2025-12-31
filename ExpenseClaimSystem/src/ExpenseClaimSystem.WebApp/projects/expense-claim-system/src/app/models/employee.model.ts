export interface Employee {
  employeeId: string;
  firstName: string;
  lastName: string;
  email: string;
  department?: string;
  position?: string;
  isActive: boolean;
}
