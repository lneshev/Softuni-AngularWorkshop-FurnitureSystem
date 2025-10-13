import type { ReactNode } from 'react';
import { Navigate } from 'react-router-dom';
import { authService } from '../services/authService';

interface SuperAdminGuardProps {
  children: ReactNode;
}

const SuperAdminGuard = ({ children }: SuperAdminGuardProps) => {
  const user = authService.getUserFromToken();
  
  if (user && user.roles[0] === 'Super Admin') {
    return <>{children}</>;
  }
  
  return <Navigate to="/" replace />;
};

export { SuperAdminGuard };
