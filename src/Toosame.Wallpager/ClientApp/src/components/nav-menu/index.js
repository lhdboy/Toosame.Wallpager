import React, { Component } from 'react';
import { Nav, Navbar, NavItem, NavDropdown, MenuItem, FormGroup, FormControl, Overlay, Popover } from 'react-bootstrap';
import './index.css';
import history from '../../history';

export class NavMenu extends Component {
    displayName = NavMenu.name

    constructor() {
        super();
        this.getPictureCatalog = this.getPictureCatalog.bind(this);
        this.getChannelList = this.getChannelList.bind(this);
        this.onSearch = this.onSearch.bind(this);
        this.state = {
            catalogDom: null,
            channelDom: null,
            searchText: '',
            overlayShow: false,
            overlayTarget: null
        };
    }

    componentDidMount() {
        this.getPictureCatalog();
        this.getChannelList();
    }

    getPictureCatalog() {
        fetch(`/api/picture/catalog`)
            .then(res => res.json())
            .then(json => {
                if (json !== null && json.length > 0) {
                    this.setState({
                        catalogDom: json.map((v, i, a) =>
                            <NavItem
                                key={v.typeId}
                                onClick={() => history.push({
                                    pathname: `/catalog`,
                                    state: {
                                        name: v.typeName
                                    },
                                })}
                            >
                                {v.typeName}
                            </NavItem>)
                    });
                }
            }).catch(e => {
                console.log(e);
            });
    }

    getChannelList() {
        fetch(`/api/channel/get`)
            .then(res => res.json())
            .then(json => {
                if (json !== null && json.length > 0) {
                    this.setState({
                        channelDom: json.map((v, i, a) =>
                            <MenuItem
                                key={v.id}
                                onClick={() => history.push({
                                    pathname: `/channel/${v.id}`,
                                    state: {
                                        name: v.name,
                                    },
                                })}
                            >
                                {v.name}
                            </MenuItem>)
                    });
                }
            }).catch(e => {
                console.log(e);
            });
    }

    onSearch(e) {
        if (e.which !== 13) {
            this.setState({ overlayShow: false });
            return;
        }

        if (this.state.searchText.trim() === '') {
            this.setState({ overlayShow: true, overlayTarget: e.target });
            return;
        }

        history.push({
            pathname: `/search`,
            state: {
                keyword: this.state.searchText
            },
        });
    }

    render() {
        return (
            <Navbar style={{ zIndex: 999, marginTop: 8 }}>
                <Navbar.Header>
                    <Navbar.Brand>
                        <a href="/">壁纸库</a>
                    </Navbar.Brand>
                    <Nav pullLeft>
                        {this.state.catalogDom}
                        <NavDropdown title="分类" id="basic-nav-dropdown">
                            {this.state.channelDom}
                        </NavDropdown>
                    </Nav>
                </Navbar.Header>
                <Navbar.Toggle />
                <Navbar.Collapse>
                    <Navbar.Form pullRight>
                        <FormGroup>
                            <FormControl
                                type="text"
                                value={this.state.searchText}
                                placeholder="搜索"
                                onChange={(e) => this.setState({ searchText: e.target.value })}
                                onKeyPress={this.onSearch}
                            />

                            <Overlay
                                show={this.state.overlayShow}
                                target={this.state.overlayTarget}
                                placement="bottom"
                                container={this}
                                containerPadding={20}
                            >
                                <Popover id="popover-contained">
                                    请输入<strong>搜索关键词</strong>
                                </Popover>
                            </Overlay>
                        </FormGroup>
                    </Navbar.Form>
                </Navbar.Collapse>
            </Navbar>
        );
    }
}
