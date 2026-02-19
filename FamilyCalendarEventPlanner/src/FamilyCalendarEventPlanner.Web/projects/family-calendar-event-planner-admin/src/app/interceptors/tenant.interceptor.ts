import { HttpInterceptorFn } from '@angular/common/http';

export const tenantInterceptor: HttpInterceptorFn = (req, next) => {
  const tenantReq = req.clone({
    headers: req.headers.set('X-Tenant-Id', '3e802e65-916e-4f2c-8068-abdd3b93dc2c')
  });

  return next(tenantReq);
};
