import React, { Component } from 'react';
import { Col, Grid, Row } from 'react-bootstrap';
import { NavMenu } from '../nav-menu';

export class Layout extends Component {
  displayName = Layout.name

  render() {
    return (
      <Grid fluid style={{ paddingBottom: 25 }}>
        <Row>
          <Col sm={12}>
            <NavMenu />
          </Col>
          <Col sm={12}>
            {this.props.children}
          </Col>
        </Row>
        <div style={{ color: '#ccc', textAlign: 'center', marginTop: 25 }}>
          本站全站源代码（包括数16万张图片数据库）开源，欢迎Pull Requests，Github:<a href="https://github.com/lhdboy/Toosame.Wallpager" target="_blank">https://github.com/lhdboy/Toosame.Wallpager</a>
        </div>
      </Grid>
    );
  }
}
