import type { ReactNode } from 'react';
import { Navigate } from 'react-router-dom';
import { authService } from '../services/authService';

interface AuthGuardProps {
  children: ReactNode;
}

const AuthGuard = ({ children }: AuthGuardProps) => {
  if (authService.isAuthenticated()) {
    return <>{children}</>;
  }
  
  return <Navigate to="/signin" replace />;
};

export { AuthGuard };
