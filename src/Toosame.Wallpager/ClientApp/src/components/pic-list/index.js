import React, { Component } from 'react';
import { ButtonToolbar, Button } from 'react-bootstrap';

export class PictureList extends Component {
    displayName = TagButton.name

    render() {
        return (
            <ButtonToolbar>
                {this.props.tags.map((v, i, a) => <Button bsStyle={this.getStyleByRandom()}>{v.tagName}</Button>)}
            </ButtonToolbar>
        );
    }
}
