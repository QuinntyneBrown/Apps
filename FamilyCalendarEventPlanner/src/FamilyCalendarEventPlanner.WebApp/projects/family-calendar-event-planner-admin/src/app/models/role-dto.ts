export interface RoleDto {
  roleId: string;
  name: string;
}

export interface CreateRoleCommand {
  name: string;
}

export interface UpdateRoleCommand {
  roleId: string;
  name: string;
}
