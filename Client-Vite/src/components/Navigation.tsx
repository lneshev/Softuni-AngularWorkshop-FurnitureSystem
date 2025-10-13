import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { authService } from '../services/authService';

const Navigation = () => {
  const [isCollapsed, setIsCollapsed] = useState(true);
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);
  const navigate = useNavigate();

  const toggleCollapse = () => {
    setIsCollapsed(!isCollapsed);
  };

  const toggleDropdown = () => {
    setIsDropdownOpen(!isDropdownOpen);
  };

  const logout = () => {
    authService.logout();
    navigate('/signin');
  };

  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-primary">
      <Link className="navbar-brand" to="/home">Furniture Store</Link>
      <button 
        className="navbar-toggler collapsed" 
        type="button" 
        onClick={toggleCollapse}
        aria-controls="navbarColor01" 
        aria-expanded={!isCollapsed} 
        aria-label="Toggle navigation"
      >
        <span className="navbar-toggler-icon"></span>
      </button>

      <div className={`navbar-collapse collapse ${!isCollapsed ? 'show' : ''}`} id="navbarColor01">
        <ul className="navbar-nav mr-auto">
          {!authService.isAuthenticated() && (
            <li className="nav-item">
              <Link className="nav-link" to="/signin">Sign In</Link>
            </li>
          )}
          {!authService.isAuthenticated() && (
            <li className="nav-item">
              <Link className="nav-link" to="/signup">Sign Up</Link>
            </li>
          )}
          {authService.isAuthenticated() && (
            <li className="nav-item">
              <a 
                className="nav-link" 
                onClick={logout}
              >
                Logout
              </a>
            </li>
          )}
          {authService.isAuthenticated() && (
            <li className={`nav-item dropdown ${isDropdownOpen ? 'show' : ''}`}>
              <a
                className="nav-link dropdown-toggle"
                onClick={toggleDropdown}
                aria-haspopup="true" 
                aria-expanded={isDropdownOpen}
              >
                Store
              </a>
              <div className={`dropdown-menu ${isDropdownOpen ? 'show' : ''}`}>
                <Link
                  className="dropdown-item"
                  to="/furniture/all"
                  onClick={() => setIsDropdownOpen(false)}
                >
                  All Furniture
                </Link>
                <Link
                  className="dropdown-item"
                  to="/furniture/user"
                  onClick={() => setIsDropdownOpen(false)}
                >
                  My Furniture
                </Link>
                <Link
                  className="dropdown-item"
                  to="/furniture/create"
                  onClick={() => setIsDropdownOpen(false)}
                >
                  Create
                </Link>
              </div>
            </li>
          )}
        </ul>
      </div>
    </nav>
  );
};

export default Navigation;