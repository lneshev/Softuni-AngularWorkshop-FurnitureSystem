import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router';
import { ToastContainer } from 'react-toastify';
import Navigation from './components/Navigation';
import Home from './components/Home';
import SignIn from './components/SignIn';
import SignUp from './components/SignUp';
import FurnitureAll from './components/FurnitureAll';
import FurnitureUser from './components/FurnitureUser';
import CreateEditFurniture from './components/CreateEditFurniture';
import FurnitureDetails from './components/FurnitureDetails';
import { AuthGuard } from './guards/AuthGuard';
import { SuperAdminGuard } from './guards/SuperAdminGuard';

function App() {
  return (
    <div style={{ textAlign: 'center' }}>
      <Router>
        <Navigation />
        <div id="content">
          <Routes>
            <Route path="/" element={<Navigate to="/home" replace />} />
            <Route path="/home" element={<Home />} />
            <Route path="/signin" element={<SignIn />} />
            <Route path="/signup" element={<SignUp />} />
            <Route
              path="/furniture/all"
              element={
                <AuthGuard>
                  <FurnitureAll />
                </AuthGuard>
              }
            />
            <Route
              path="/furniture/user"
              element={
                <AuthGuard>
                  <FurnitureUser />
                </AuthGuard>
              }
            />
            <Route
              path="/furniture/create"
              element={
                <AuthGuard>
                  <CreateEditFurniture />
                </AuthGuard>
              }
            />
            <Route
              path="/furniture/edit/:id"
              element={
                <AuthGuard>
                  <SuperAdminGuard>
                    <CreateEditFurniture />
                  </SuperAdminGuard>
                </AuthGuard>
              }
            />
            <Route
              path="/furniture/details/:id"
              element={
                <AuthGuard>
                  <FurnitureDetails />
                </AuthGuard>
              }
            />
          </Routes>
        </div>
      </Router>
      <ToastContainer
        position="top-right"
        autoClose={5000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
      />
    </div>
  );
}

export default App;