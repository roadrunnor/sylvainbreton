import { jsx as _jsx } from "react/jsx-runtime";
import { Component } from 'react';
import Header from '../components/Header'; // Adjust the path based on your directory structure
class App extends Component {
    render() {
        return (_jsx("div", { children: _jsx(Header, {}) }));
    }
}
export default App;
//# sourceMappingURL=App.js.map