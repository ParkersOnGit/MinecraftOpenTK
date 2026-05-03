#version 330 core

out vec4 FragColor;

uniform sampler2D texture0;

in vec2 textCoord;

void main()
{
	FragColor = texture(texture0, textCoord);
}