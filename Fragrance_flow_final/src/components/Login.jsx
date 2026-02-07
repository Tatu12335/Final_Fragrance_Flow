
import { useState } from 'react'


function Login({ guest = "Guest" }) {
  
    const [password, setPassword] = useState("");
  
    const handleSubmit = (e) => {
      e.preventDefault();
      onLogin(password);
    }


    
   // if (password.length > 8) {
    return (
    <div className="login-container">
      <h2>Login ✈️</h2>
      <form className="login-form" method="POST" onSubmit={handleSubmit}>
        <label htmlFor="username">Username:</label>
        <br></br>
        <input type="text" id="username" name="username" required />
        <br></br>
        <label htmlFor="password">Password:</label>
        <input type="password" id="password" name="password" required onChange={(e) => setPassword(e.target.value)}/>
        <br />
        <button type="submit" className="login-button">Login ✈️</button>
        </form>
    </div>
  );
  }

export default Login