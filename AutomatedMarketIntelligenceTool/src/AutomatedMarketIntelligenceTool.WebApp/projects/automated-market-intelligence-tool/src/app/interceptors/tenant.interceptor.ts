import { HttpInterceptorFn } from '@angular/common/http';

export const tenantInterceptor: HttpInterceptorFn = (req, next) => {
  const tenantId = '3e802e65-916e-4f2c-8068-abdd3b93dc2c';

  const cloned = req.clone({
    setHeaders: {
      'X-Tenant-Id': tenantId
    }
  });

  return next(cloned);
};
