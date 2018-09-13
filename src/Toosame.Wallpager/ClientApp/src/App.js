import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/layout';
import { Home } from './pages/home';
import { Search } from './pages/search';
import { Tag } from './pages/tag';
import { Channel } from './pages/channel';
import { Catalog } from './pages/catalog';

export default class App extends Component {
    displayName = App.name

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/search' component={Search} />
                <Route path='/tag/:tag' component={Tag} />
                <Route path='/channel/:id' component={Channel} />
                <Route path='/catalog' component={Catalog} />
            </Layout>
        );
    }
}
