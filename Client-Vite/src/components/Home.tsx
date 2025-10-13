import { useState, useEffect } from 'react';
import { authService } from '../services/authService';

const Home = () => {
  const [username, setUsername] = useState<string>('');

  useEffect(() => {
    const user = authService.getUserFromToken();
    setUsername(user.userName);
  }, []);

  return (
    <div>
      <h1>
        Hello {username}!
      </h1>
    </div>
  );
};

export default Home;