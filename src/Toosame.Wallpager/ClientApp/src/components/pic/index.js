import React, { Component } from 'react';
import { Image, } from 'react-bootstrap';

export class Picture extends Component {
    displayName = Picture.name

    render() {
        return (
            <div>
                <Image rounded style={{ maxWidth: '300px', maxHeight: '300px' }} src={this.props.data.picPreview} />
                <h4>{this.props.data.picName}</h4>
                <p>{this.props.data.picNum}张 · <span style={{ color: '#7d7d7d' }}>{this.props.data.picSize}</span></p>
            </div>
        );
    }
}
